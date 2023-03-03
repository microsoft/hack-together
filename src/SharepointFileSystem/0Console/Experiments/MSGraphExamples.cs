using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace dotnet_console_microsoft_graph.Experiments;

internal static class MSGraphExamples {
    public static async Task Main(GraphServiceClient betaGraphClient) {
        //// Other TokenCredentials examples are available at https://github.com/microsoftgraph/msgraph-sdk-dotnet/blob/dev/docs/tokencredentials.md
        //var scopes = new[] { "User.Read", "Mail.Read", "User.ReadBasic.All" };
        //var interactiveBrowserCredentialOptions = new InteractiveBrowserCredentialOptions {
        //    ClientId = "CLIENT_ID"
        //};
        //var tokenCredential = new InteractiveBrowserCredential(interactiveBrowserCredentialOptions);

        // GraphServiceClient constructor accepts tokenCredential
        //var v1GraphClient = new GraphServiceClient(tokenCredential, scopes);// client for the v1.0 endpoint
        //var betaGraphClient = new Microsoft.Graph.GraphServiceClient(tokenCredential, scopes);// client for the beta endpoint

        // Perform batch request using the beta client
        await PerformRequestWithHeaderAndQueryRequestAsync(betaGraphClient);

        // Perform batch request using the beta client
        await PerformCustomRequestWithHeaderAndQueryAsync(betaGraphClient);

        // Perform batch request using the v1 client
        await PerformBatchRequestAsync(betaGraphClient);

        // Perform paged request using the v1 client
        await IteratePagedDataAsync(betaGraphClient);
    }
    public static async Task ShowTenantUsersAsync(GraphServiceClient graphClient) {
        try {
            // Get the requestInformation to make a GET request
            var requestInformation = graphClient
                                     .DirectoryObjects
                                     .ToGetRequestInformation();
            Console.WriteLine("requestInformation.URI=" + requestInformation.URI);

            // get all users on tenant
            var users = await graphClient.Users.GetAsync(
                requestConfiguration => requestConfiguration.QueryParameters.Select = new string[] { "id", "displayName", "mail" });
            if (users != null && users.Value != null) {
                foreach (var user in users.Value) {
                    if (user == null) continue;
                    Console.WriteLine($"User({user.Id}):Name:{user.DisplayName}:{user.Mail}");
                }
            }
        }
        catch (Microsoft.Graph.Models.ODataErrors.ODataError ex) {
            Console.WriteLine($"Error({ex?.Error?.Code}):{ex?.Error?.Message}");
        }
        catch (AuthenticationFailedException ex) {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }
    }

    private static async Task PerformBatchRequestAsync(GraphServiceClient graphClient) {
        Console.WriteLine("-----------Performing batch requests-----------");
        var userRequest = graphClient.Me.ToGetRequestInformation();// create request object to get user information
        var messagesRequest = graphClient.Me.Messages.ToGetRequestInformation();// create request object to get user messages

        // Build the batch
        var batchRequestContent = new BatchRequestContent(graphClient);
        var userRequestId = await batchRequestContent.AddBatchRequestStepAsync(userRequest);
        var messagesRequestId = await batchRequestContent.AddBatchRequestStepAsync(messagesRequest);

        // Send the batch
        var batchResponse = await graphClient.Batch.PostAsync(batchRequestContent);

        // Get the user info
        var user = await batchResponse.GetResponseByIdAsync<User>(userRequestId);
        Console.WriteLine($"Fetched user with name {user.DisplayName} via batch");

        // Get the messages data
        var messagesResponse = await batchResponse.GetResponseByIdAsync<MessageCollectionResponse>(messagesRequestId);
        List<Message> messages = messagesResponse.Value;
        Console.WriteLine($"Fetched {messages.Count} messages via batch");
        Console.WriteLine("-----------Done with batch requests-----------");
    }

    private static async Task IteratePagedDataAsync(GraphServiceClient graphClient) {
        Console.WriteLine("-----------Performing paged requests-----------");
        var firstPage = await graphClient.Me.Messages.GetAsync();// fetch first paged of messages

        var messagesCollected = new List<Message>();
        // Build the pageIterator
        var pageIterator = PageIterator<Message, MessageCollectionResponse>.CreatePageIterator(
            graphClient,
            firstPage,
            message => {
                messagesCollected.Add(message);
                return true;
            },// per item callback
            request => {
                Console.WriteLine($"Requesting new page with url {request.URI.OriginalString}");
                return request;
            }// per request/page callback to reconfigure the request
        );

        // iterated
        await pageIterator.IterateAsync();

        // Get the messages data;
        Console.WriteLine($"Fetched {messagesCollected.Count} messages via page iterator");
        Console.WriteLine("-----------Done with paged requests-----------");
    }

    private static async Task PerformRequestWithHeaderAndQueryRequestAsync(Microsoft.Graph.GraphServiceClient graphClient) {
        Console.WriteLine("-----------Performing configured requests-----------");

        var userResponse = await graphClient.Users.GetAsync(requestConfiguration => {
            requestConfiguration.QueryParameters.Select = new[] { "id", "displayName" };// set select
            requestConfiguration.QueryParameters.Filter = "startswith(displayName, 'al')";// set filter for users displayName starting with 'al'
            requestConfiguration.QueryParameters.Count = true;
            requestConfiguration.Headers.Add("ConsistencyLevel", "eventual");//set the header
        });

        Console.WriteLine($"Fetched {userResponse.Value.Count} users with displayName starting with 'al'");
        Console.WriteLine("-----------Done with configured requests-----------");
    }

    private static async Task PerformCustomRequestWithHeaderAndQueryAsync(Microsoft.Graph.GraphServiceClient graphClient) {
        Console.WriteLine("-----------Performing customized request-----------");

        var requestInformation = graphClient.Users.ToGetRequestInformation(requestConfiguration => {
            requestConfiguration.QueryParameters.Select = new[] { "id", "displayName" };// set select
            requestConfiguration.QueryParameters.Filter = "startswith(displayName, 'al')";// set filter for users displayName starting with 'al'
            requestConfiguration.QueryParameters.Count = true;
            requestConfiguration.Headers.Add("ConsistencyLevel", "eventual");//set the header
        });

        var userResponse = await graphClient.RequestAdapter.SendAsync<Microsoft.Graph.Models.UserCollectionResponse>(
                requestInformation, Microsoft.Graph.Models.UserCollectionResponse.CreateFromDiscriminatorValue);

        Console.WriteLine($"Fetched {userResponse.Value.Count} users with displayName starting with 'al'");
        Console.WriteLine("-----------Done with customized requests-----------");
    }
}