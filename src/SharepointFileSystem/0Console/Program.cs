using dotnet_console_microsoft_graph.Experiments;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models;
using MSGraphAuth;

/// This sample shows how to query the Microsoft Graph from a daemon application
/// which uses application permissions.
/// 
/// The extended project goal is to provide a Sharepoint System.IO.File and Folder abstraction for integration services
/// 

/*
 Application permissions, 
follow the instructions in .\readme.md

[X] connect to graph as daemon
[X] list AAD users
[X] list sharepoint sites
[X] list drive(s)
[] list folders
[] list folder subfolders
[] list folder documents
[] perform CRUD on folder
[] perform CRUD on document
 */

var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<Program>()
            .Build();

//connect to sharepoint
var ClientAppName = config["ClientAppName"];
var ClientAppShortName = config["ClientAppShortName"];
var Instance = config["Instance"];
var ApiUrl = config["ApiUrl"];
var Tenant = config["Tenant"];
var TenantId = config["TenantId"];
var ClientId = config["ClientId"];
var ClientSecret = config["ClientSecret"];
var sharepointSiteId = config["SharepointSiteId"];
var sharepointDriveId = config["SharepointDriveId"];

//scopes are not required as this is a deamon app, and they are specified in AAD, they are listed here as a reminder to set them using the Azure Portal
var scopes = new[] {"offline_access"
    ,"SharePointTenantSettings.ReadWrite.All"
    ,"Directory.ReadWrite.All"
    ,"Sites.Read.All"
    ,"Files.ReadWrite.All"
    ,"User.Read.All"
    ,"BrowserSiteLists.ReadWrite.All"
    , "openid", "profile", "User.Read" };

var client = new OAuth2ClientCredentialsGrantService(
    ClientId, ClientSecret, Instance, Tenant, TenantId, ApiUrl
    , null);
var graphClient = client.GetClientSecretClient();
await MSGraphExamples.ShowTenantUsersAsync(graphClient);
await SharepointExamples.GetAllSharepointSitesAsync(graphClient);
await SharepointExamples.GetSharepointSiteAsync(graphClient, sharepointSiteId, sharepointDriveId);

//Next steps
//connect to SPDocuments folder
//create folder topfldr
//topfldr exists? - expect: success
//create child folder topfolder/childfolder
//repeat create child folder topfolder/childfolder - expect success
//topfolder/childfolder exists? - expect: success
//upload new document topdoc.docx to topfldr (hint: clone TmpDoc.docx to topdoc.docx)
//upload new document topdoc.docx to topfldr - expect success
//topfolder/topdoc.docx exists?
//upload new document childdoc.docx to topfolder/childfolder
//topfolder/childfolder/childoc.docx exists?
//missingFolder exists? expect:  - expect exception
//missingFolder/missingFolder  does not exist - expect exception
//topfolder/missingFolder  does not exist - expecte exception
//topfolder/missing.docx  does not exist - expecte exception
//topfolder/missingFolder/missing.docx  does not exist - expecte exception
//download topfolder/childfolder/childoc.docx to local filesystem

