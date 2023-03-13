# .NET Core MVC (Model - View - Controller) web app connected to Microsoft Graph

This is a template for .NET Core MVC (Model - View - Controller) web app that connects to Microsoft Graph.

## Minimal Path to Awesome 🚀

Follow the instructions to successfully run your MVC app with Microsoft Graph.

## Visual Studio

Install [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/) with ASP.NET and web development workload if you haven't already:
![ASP.NET Workload with Visual Studio](/templates/dotnet-core-mvc-web-app-microsoft-graph/aspnetworkload.png)

### 1. Connect with Microsoft Identity Platform

Every app that uses Azure AD for authentication must be registered with Azure AD. For your .NET core MVC web app, you may connect your app with Microsoft Identity Platform through Visual Studio 2022. Please follow the instructions to connect your app with Microsoft Identity Platform:

* Clone the **Hack Together** repository to your local workspace or directly download the source code.
* Open the project folder `dotnet-core-mvc-web-app-microsoft-graph` and double click to `NETCoreMVCwithMSGraph.sln` file to open your project with Visual Studio.
* Select **Connected Services** from the project root, and then direct to Connected Services tab, select three dots icon next to **Microsoft identity platform**.
![Connected Services](/templates/dotnet-core-mvc-web-app-microsoft-graph/visualstudio-identity-connect.png)
* Login with your developer account and select your tenant. Give a name for your app and then select **Register**.
![Azure Active Directory Registration](/templates/dotnet-core-mvc-web-app-microsoft-graph/visualstudio-aad-registration.png)
* Make sure to select **Add Microsoft Graph permissions** and select **Next**.
![Connect Microsoft Graph permissions](/templates/dotnet-core-mvc-web-app-microsoft-graph/add-msgraph.png)
* Select **Local user secret file: Secret.json(local)**, then select **Next**.
![Create secret](/templates/dotnet-core-mvc-web-app-microsoft-graph/aad-secret.png)

### 2. Run your .NET Core MVC web app

* Select **https** run button to start your app on your machine.

## Visual Studio Code 🚀

### 1. Get ClientId, TenantId and Domain

* Register application for user authentication ([How to do it](https://learn.microsoft.com/en-us/graph/tutorials/dotnet?tabs=aad&tutorial-step=1)). 
* Store data in `appsettings.json`

### 2. Add credentials to secrets.json

```sh
dotnet user-secrets init
dotnet user-secrets set "AzureAD:ClientId" "Your_Azure_AD_Client_Id"
dotnet user-secrets set "AzureAD:ClientSecret" "Your_Azure_AD_Client_Secret"
```

### 3. Run app

```sh
dotnet run
```

When your .NET Core MVC web app initiates, log in with your developer account.

After the login, you'll see your email address mentioned on navigation bar of your app:

![MVC App](/templates/dotnet-core-mvc-web-app-microsoft-graph/netcoreMVC.png)

## Reference

* [Quickstart: Register an application with the Microsoft identity platform](https://learn.microsoft.com/azure/active-directory/develop/quickstart-register-app)
