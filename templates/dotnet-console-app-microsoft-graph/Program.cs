using Azure.Identity;
using Microsoft.Graph;

var scopes = new[] { "User.Read" };
var interactiveBrowserCredentialOptions = new InteractiveBrowserCredentialOptions
{
  ClientId = "CLIENT_ID"
};
var tokenCredential = new InteractiveBrowserCredential(interactiveBrowserCredentialOptions);

var graphClient = new GraphServiceClient(tokenCredential, scopes);

var me = await graphClient.Me.GetAsync();
Console.WriteLine($"Hello {me?.DisplayName}!");
