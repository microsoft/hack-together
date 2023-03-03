# .NET v7.0 console app connected to Microsoft Graph

This a project based on the MSGraph Console App template that connects to Microsoft Graph.


## Minimal Path to Awesome 🚀

Follow the instructions to successfully run your Console app with Microsoft Graph.

### 1. Register an Azure Active Directory app

Every app that uses Azure AD for authentication must be registered with Azure AD. You can register app through Azure Portal or by using Azure CLI. Please follow one of the options to register your app:

<details>
  <summary>Register an app by using Azure CLI on the Azure Portal</summary>
* run AppCreationScripts, it will update appsettings.json
* Open Powershell as admin, open RegisterApp.ps1
* copy into new ps file
* change the values as per your Azure
* run the script
* do not commit the secret values into git, move sensitive data out of appsettings.json
* cut the values from appsettings.json into user secrets
* ensure there are only blank values in appsettings.json
* Open Azure Portal AAD|Enterprise Applications blade: https://portal.azure.com/#view/Microsoft_AAD_IAM/StartboardApplicationsMenuBlade/~/AppAppsPreview/menuId~/null
* open your app
* open permissions
* to request additional permissions for this application, use the 'application registration​'
* grant the following scopes: 
 - SharePointTenantSettings.ReadWrite.All
 - Directory.ReadWrite.All
 - Sites.Read.All
 - Files.ReadWrite.All
 - User.Read.All
 - BrowserSiteLists.ReadWrite.All
* go back to your app ( back button on history)
* Grant admin Consnet for {Tenant name}
</details>

### 2. Run your Console app

* run the application
* create a sharepoint site, list, subfolder, add a document
* 2nd time: you can choose a SiteId and DriveId to 'work on'
* add SharepointSiteId and SharepointDriveId to UserSecrets too.

## Reference

* [Quickstart: Register an application with the Microsoft identity platform](https://learn.microsoft.com/en-us/azure/active-directory/develop/quickstart-register-app)
* [MSGraph v5 upgrade notes, so v4 examples will also work](https://github.com/microsoftgraph/msgraph-sdk-dotnet/blob/feature/5.0/docs/upgrade-to-v5.md)
* []()
