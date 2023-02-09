# .NET v7.0 UWP (Universal Windows Platform) app connected to Microsoft Graph

This is a template for Universal Windows Platform app that connects to Microsoft Graph.

## Minimal Path to Awesome 🚀

Follow the instructions to successfully run your UWP app with Microsoft Graph.

Install [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/) with Universal Windows Platform development workload if you haven't already:
![UWP Workload with Visual Studio](/templates/dotnet-uwp-app-microsoft-graph/uwp-workload.png)

### 1. Register an Azure Active Directory app

Every app that uses Azure AD for authentication must be registered with Azure AD. You can register app through Azure Portal or by using Azure CLI. Please follow one of the options to register your app:

<details>
  <summary>Option 1: Register an app by using Azure CLI</summary>
  
#### Register an app by using Azure CLI

* [Install Azure CLI](https://learn.microsoft.com/cli/azure/install-azure-cli?view=azure-cli-latest) if you haven't already.
* Register your app on Microsoft Azure, by creating a new Azure AD app registration:
  * On macOS/Linux/in Bash:
    * Open terminal and change the working directory to the root of this project
    * To make the setup script executable, run `chmod +x ./setup.sh`
    * To register the app, run `./setup.sh`
    * When prompted, sign in with your **Microsoft 365 developer sandbox account**
  * On Windows/in PowerShell:
    * Open PowerShell and change the working directory to the root of this project
    * To register the app, run `.\setup.ps1`
    * When prompted, sign in with your **Microsoft 365 developer sandbox account**

</details>

<details>

  <summary>Option 2: Register an app through Azure Portal</summary>
  
#### Register your app through Azure Portal

* Go to [Azure Portal](https://portal.azure.com) and login with your testing account that has Application developer or administrator permissions.
* Select **Azure Active Directory**, and select **App Registrations** from the left side bar. Then select **+ New registration**.
* Give any name to your app. For **Supported account types**, select **Accounts in any organizational directory (Any Azure AD directory - Multitenant) and personal Microsoft accounts (e.g. Skype, Xbox)**.
* Set the **Redirect URI** drop down to **Public client/native (mobile & desktop)** and enter `https://login.microsoftonline.com/common/oauth2/nativeclient`. Then, select **Register**.

Navigate to **Overview tab** and make a note of the **Application (client) ID**. You'll use them in the next steps.

</details>

### 2. Run your UWP Server app

* Clone the Hack Together repository to your local workspace or directly download the source code.
* Open the project folder `dotnet-uwp-app-microsoft-graph` and double click to `UWP-app-MSGraph.sln` file to open your project with Visual Studio.
* Navigate to your UWP project, and select *MainPage.xaml.cs* file, replace "client-id-from-app-registration" with `Application (client) ID` that you copied from your Azure Active Directory app.
* Select **Local Machine** button to run your app on your machine.

In your UWP app, log in using an Azure AD user account.

After the login, you'll see your email address on the panel of your app:

![UWP App](/templates/dotnet-uwp-app-microsoft-graph/uwp.png)

## Reference

* [Tutorial: Call the Microsoft Graph API from a Universal Windows Platform (UWP) application](https://learn.microsoft.com/en-us/azure/active-directory/develop/tutorial-v2-windows-uwp)
* [Quickstart: Register an application with the Microsoft identity platform](https://learn.microsoft.com/en-us/azure/active-directory/develop/quickstart-register-app)
