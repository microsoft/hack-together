
using Azure.Core;
using Azure.Identity;
using CommunityToolkit.Diagnostics;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;

namespace MSGraphAuth {
    public class OAuth2ClientCredentialsGrantService {
        private readonly IEnumerable<string> scopes;
        private readonly AuthenticationConfig config;
        private readonly TokenCredential? tokenCredential;

        public string Authtoken { get; set; }
        private IConfidentialClientApplication app;

        /// <summary>
        /// Constructs a new <see cref="OAuth2ClientCredentialsGrantService"/>.
        /// </summary>
        /// <param name="clientid">MS Graph application clientid uid</param>
        /// <param name="clientsecret">MS Graph application clientsecret</param>
        /// <param name="scopes">List of scopes for the authentication context</param>
        /// <param name="instance">combined with tenant to create Authority Uri</param>
        /// <param name="baseUrl">The base service URL. For example, "https://graph.microsoft.com/v1.0"</param>
        public OAuth2ClientCredentialsGrantService(string? clientid, string? clientsecret, string? instance, string? tenant, string? tenantid, string? apiUrl, IEnumerable<string>? scopes = null) {
            // With client credentials flows the scopes is ALWAYS of the shape "resource/.default", as the 
            // application permissions need to be set statically (in the portal or by PowerShell), and then granted by
            // a tenant administrator. 
            //		Message	"AADSTS1002012: The provided value for scope Sites.Manage.All offline_access openid profile User.Read is not valid. Client credential flows must have a scope value with /.default suffixed to the resource identifier (application ID URI).
            this.scopes = scopes?.ToArray() ?? new string[] { $"{apiUrl}.default" };
            //ThrowHelper.ThrowArgumentNullException(clientid, nameof(clientid));
            //ThrowHelper.ThrowArgumentNullException(clientsecret, nameof(clientsecret));
            //ThrowHelper.ThrowArgumentNullException(instance, nameof(instance));
            //ThrowHelper.ThrowArgumentNullException(tenant, nameof(tenant));
            //ThrowHelper.ThrowArgumentNullException(apiUrl, nameof(apiUrl));
            config = new AuthenticationConfig {
                ClientId = clientid,
                ClientSecret = clientsecret,
                Instance = instance,
                Tenant = tenant,
                ApiUrl = apiUrl,
                TenantId = tenantid,
            };
        }
        public GraphServiceClient GetClientSecretClient() {

            var options = new TokenCredentialOptions {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            // https://learn.microsoft.com/dotnet/api/azure.identity.clientsecretcredential
            var clientSecretCredential = new ClientSecretCredential(
                config.TenantId, config.ClientId, config.ClientSecret, options);

            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);
            return graphClient;
        }

        public GraphServiceClient GetTokenClient() {
            // using Azure.Identity;
            var options = new TokenCredentialOptions {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            // https://learn.microsoft.com/dotnet/api/azure.identity.clientsecretcredential
            var clientSecretCredential = new ClientSecretCredential(
                config.TenantId, config.ClientId, config.ClientSecret, options);

            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);
            return graphClient;
        }
        public async Task<GraphServiceClient> GetTokenClientStoreTokenAsync() {
            var options = new ClientSecretCredentialOptions {
                TokenCachePersistenceOptions = new TokenCachePersistenceOptions { UnsafeAllowUnencryptedStorage = true }
            };
            var creds = new ClientSecretCredential(
                config.TenantId, config.ClientId, config.ClientSecret
                );
            var graphServiceClient = new GraphServiceClient(creds);
            return graphServiceClient;
        }

        public async Task<IConfidentialClientApplication> ConnectSetTokenAsync() {

            // Even daemon application is a confidential client application
            app = ConfidentialClientApplicationBuilder.Create(config.ClientId)
            //IConfidentialClientApplication app = ConfidentialClientApplicationBuilder.Create(config.ClientId)
                .WithClientSecret(config.ClientSecret)
                .WithAuthority(new Uri(config.Authority))
                .Build();

            app.AddInMemoryTokenCache();

            AuthenticationResult result = null;
            try {
                result = await app.AcquireTokenForClient(scopes)
                                 .ExecuteAsync();
            }
            catch (MsalUiRequiredException ex) {
                // The application doesn't have sufficient permissions.
                // - Did you declare enough app permissions during app creation?
                // - Did the tenant admin grant permissions to the application? Admin will need to go here:
                throw new Exception($"Admin to go here: https://login.microsoftonline.com/common/oauth2/v2.0/authorize?client_id={config.ClientId}&response_type=code&redirect_uri=http://localhost/myapp/&response_mode=query&scope=openid%20offline_access%20https%3A%2F%2Fgraph.microsoft.com%2Fmail.read&state=12345", ex);//TODO serialize scopes
                throw;
            }
            catch (MsalServiceException ex) when (ex.Message.Contains("AADSTS70011")) {
                // Invalid scope. The scope has to be in the form "https://resourceurl/.default"
                // Mitigation: Change the scope to be as expected.
                throw;
            }
            catch (Exception ex) {
                throw;
            }
            Authtoken = result.AccessToken;

            // With client credentials flows the scopes is ALWAYS of the shape "resource/.default", as the 
            // application permissions need to be set statically (in the portal or by PowerShell), and then granted by
            // a tenant administrator. 
            //string[] scopes = new string[] { $"{config.ApiUrl}.default" }; // Generates a scope -> "https://graph.microsoft.com/.default"

            //// Call MS graph using the Graph SDK
            //await CallMSGraphUsingGraphSDK(app, scopes);

            //// Call MS Graph REST API directly
            //await CallMSGraph(config, app, scopes);

            return app;
        }
       
        /*
    public async Task<GraphServiceClient> GetInteractiveClientAsync() {
    var interactiveBrowserCredentialOptions = new InteractiveBrowserCredentialOptions {
        TenantId = config.TenantId,
            ClientId = config.ClientId,
        };
        var interactiveBrowserCredential = new InteractiveBrowserCredential(interactiveBrowserCredentialOptions);
        var graphServiceClient = new GraphServiceClient(interactiveBrowserCredential);
        return graphServiceClient;
        }
        /// <summary>
        /// Calls MS Graph REST API using an authenticated Http client
        /// </summary>
        /// <param name="config"></param>
        /// <param name="app"></param>
        /// <param name="scopes"></param>
        /// <returns></returns>
        private static async Task CallMSGraph(AuthenticationConfig config, IConfidentialClientApplication app, string[] scopes) {
            AuthenticationResult result = null;
            try {
                result = await app.AcquireTokenForClient(scopes)
                    .ExecuteAsync();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Token acquired");
                Console.ResetColor();
            }
            catch (MsalServiceException ex) when (ex.Message.Contains("AADSTS70011")) {
                // Invalid scope. The scope has to be of the form "https://resourceurl/.default"
                // Mitigation: change the scope to be as expected              
                throw new Exception("Scope provided is not supported", ex);
            }

            // The following example uses a Raw Http call 
            if (result != null) {
                var httpClient = new HttpClient();
                var apiCaller = new ProtectedApiCallHelper(httpClient);
                await apiCaller.CallWebApiAndProcessResultASync($"{config.ApiUrl}v1.0/users", result.AccessToken, Display);

            }
        }

        /// <summary>
        /// The following example shows how to initialize the MS Graph SDK
        /// </summary>
        /// <param name="app"></param>
        /// <param name="scopes"></param>
        /// <returns></returns>
        private static async Task CallMSGraphUsingGraphSDK(IConfidentialClientApplication app, string[] scopes) {
            // Prepare an authenticated MS Graph SDK client
            GraphServiceClient graphServiceClient = GetAuthenticatedGraphClient(app, scopes);


            List<User> allUsers = new List<User>();

            try {

                var users = await graphServiceClient.Users.Request().Count();
                Console.WriteLine($"Found {users.Count()} users in the tenant");
            }
            catch (ServiceException e) {
                Console.WriteLine("We could not retrieve the user's list: " + $"{e}");
            }

        }


        /// <summary>
        /// An example of how to authenticate the Microsoft Graph SDK using the MSAL library
        /// </summary>
        /// <returns></returns>
        private static GraphServiceClient GetAuthenticatedGraphClient(IConfidentialClientApplication app, string[] scopes) {

            GraphServiceClient graphServiceClient =
                    new GraphServiceClient("https://graph.microsoft.com/V1.0/", new DelegateAuthenticationProvider(async (requestMessage) => {
                        // Retrieve an access token for Microsoft Graph (gets a fresh token if needed).
                        AuthenticationResult result = await app.AcquireTokenForClient(scopes)
                            .ExecuteAsync();

                        // Add the access token in the Authorization header of the API request.
                        requestMessage.Headers.Authorization =
                            new AuthenticationHeaderValue("Bearer", result.AccessToken);
                    }));

            return graphServiceClient;
        }

        public GraphServiceClient GetAuthenticatedGraphClientv2(Uri uri, Dictionary<string, object> additionalAuthenticationContext = default,
            CancellationToken cancellationToken = default) {
            var authenticationProvider = new BaseBearerTokenAuthenticationProvider(new MSGraphAuth.TokenProvider());
            var graphServiceClient = new GraphServiceClient(authenticationProvider);
            return graphServiceClient;
        }

        /// <summary>
        /// Display the result of the Web API call
        /// </summary>
        /// <param name="result">Object to display</param>
        private static void Display(JsonNode result) {
            JsonArray nodes = ((result as JsonObject).ToArray()[1]).Value as JsonArray;

            foreach (JsonObject aNode in nodes.ToArray()) {
                foreach (var property in aNode.ToArray()) {
                    Console.WriteLine($"{property.Key} = {property.Value?.ToString()}");
                }
                Console.WriteLine();
            }
        }
        */

    }
}

