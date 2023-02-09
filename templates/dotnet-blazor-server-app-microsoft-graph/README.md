# .NET v7.0 Blazor Server app connected to Microsoft Graph

This is a template for a Blazor app built using .NET v7.0 that connects to Microsoft Graph.

## Minimal Path to Awesome 🚀

Follow the instructions to successfully run your Blazor Server app with Microsoft Graph.

### Register an app in the Azure portal

Every app that uses Azure AD for authentication must be registered with Azure AD. Follow the instructions in Register an application with these additions:

* Go to [Azure Portal](https://portal.azure.com) and login with your testing account that has Application developer or administrator permissions.
* Select **Azure Active Directory**, and select **App Registrations** from the left side bar. Then select **+ New registration**.
* Give any name to your app. For **Supported account types**, select **Accounts in this organizational directory only**.
* Set the **Redirect URI** drop down to **Web** and enter `https://localhost:5001/signin-oidc`. Then, select **Register**.
* Select **Authentication** tab in your registered app, go to **Implicit grant and hybrid flows** section, select `Access tokens` and `ID tokens`, and then select **Save**.

* Select **Certificates & secrets** tab in your registered app, and then **Client secrets**. Create a **New client secret** that never expires.

Make note of the **secret's value** as you'll use it in the next step. You can’t access it again once you navigate away from this pane. However, you can recreate it as needed.

### Run the Blazor Server app

* Clone the Hack Together repository to your local workspace or directly download the source code.
* Open the project folder `dotner-blazor-server-app-microsoft-graph` with the editor of your choice. (Visual Studio Code is recommended.)
* Navigate to your Blazor app in your editor and add the client secret to the *appsettings.json* file, replacing the text "secret-from-app-registration".
* In your terminal, run the following command:

```dotnetcli
dotnet run
```

In your browser, navigate to `https://localhost:5001` , and log in using an Azure AD user account to see the app running.

After the login, you'll see your email address on the panel of your app:

![Blazor Server App](/templates/dotnet-blazor-server-app-microsoft-graph/blazorServerApp.png)

Navigate to *Pages* folder in your app, and then *ShowProfile.razor* page. Observe the Microsoft Graph API request in the code to retrieve user email:

```csharp

@page "/showprofile"

@using Microsoft.Identity.Web
@using Microsoft.Graph
@inject Microsoft.Graph.GraphServiceClient GraphServiceClient
@inject MicrosoftIdentityConsentAndConditionalAccessHandler ConsentHandler

<h1>Me</h1>

<p>This component demonstrates fetching data from a service.</p>

@if (user == null)
{
    <p><em>Loading...</em></p>
}
else
{
    <table class="table table-striped table-condensed" style="font-family: monospace">
        <tr>
            <th>Property</th>
            <th>Value</th>
        </tr>
        <tr>
            <td>Name</td>
            //user profile information is shown in the UI
            <td>@user.DisplayName</td> 
        </tr>
    </table>
}

@code {
    User? user;

    protected override async Task OnInitializedAsync()
    {
        try
        {    //Microsoft Graph API request to retrieve user profile
            user = await GraphServiceClient.Me.Request().GetAsync(); 
        }
        catch (Exception ex)
        {
            ConsentHandler.HandleException(ex);
        }
    }
}
```

## Reference

* [Tutorial: Create a Blazor Server app that uses the Microsoft identity platform for authentication](https://learn.microsoft.com/en-us/azure/active-directory/develop/tutorial-blazor-server)
