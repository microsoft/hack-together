# .NET v7.0 console app connected to Microsoft Graph

This is a template for a console application built using .NET v7.0 that connects to Microsoft Graph.

## Minimal Path to Awesome 🚀

Follow the instructions to successfully run your Console app with Microsoft Graph.

### 1. Register an Azure Active Directory app

Every app that uses Azure AD for authentication must be registered with Azure AD. You can register app through Azure Portal or by using Azure CLI. Please follow one of the options to register your app:

<details>
  <summary>Option 1: Register an app by using Azure CLI</summary>

* [Install Azure CLI](https://learn.microsoft.com/cli/azure/install-azure-cli?view=azure-cli-latest) if you haven't already.
* Register your app on Microsoft Azure, by creating a new Azure AD app registration:
  * <details>
      <summary>On macOS/Linux/in Bash</summary>

    * Open terminal and change the working directory to the root of this project
    * To make the setup script executable, run `chmod +x ./setup.sh`
    * To register the app, run `./setup.sh`
    * When prompted, sign in with your **Microsoft 365 developer sandbox account**

    </details>
  * <details>
      <summary>On Windows/in PowerShell</summary>

    * Open PowerShell and change the working directory to the root of this project
    * To register the app, run `.\setup.ps1`
    * When prompted, sign in with your **Microsoft 365 developer sandbox account**

    </details>

</details>

<details>

  <summary>Option 2: Register an app through Azure Portal</summary>

* Go to [Azure Portal](https://portal.azure.com) and login with your testing account that has Application developer or administrator permissions.
* Select **Azure Active Directory**, and select **App Registrations** from the left side bar. Then select **+ New registration**.
* Give any name to your app. For **Supported account types**, select **Accounts in any organizational directory (Any Azure AD directory - Multitenant)**.
* Set the **Redirect URI** drop down to **Public client/native (mobile & desktop)** and enter `http://localhost`. Then, select **Register**.

Navigate to **Overview** tab and make a note of the **Application (client) ID**. You'll use it in the next steps.

</details>

### 2. Run your Console app

* Clone the Hack Together repository to your local workspace or directly download the source code.
* Open the project folder `dotnet-console-app-microsoft-graph` with the editor of your choice. (Visual Studio Code is recommended.)
* In Visual Studio Code, press F5 to run the app.
  ![App output in the debug console in VSCode](./screenshot.png)

## Reference

* [Quickstart: Register an application with the Microsoft identity platform](https://learn.microsoft.com/en-us/azure/active-directory/develop/quickstart-register-app)
