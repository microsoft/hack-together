using Azure.Identity;
using Microsoft.Graph;

namespace dotnet_console_microsoft_graph.Experiments;

internal static class SharepointExamples {
    public static async Task Main(GraphServiceClient betaGraphClient) {
        await GetAllSharepointSitesAsync(betaGraphClient);
    }

    public static async Task GetAllSharepointSitesAsync(GraphServiceClient graphClient) {
        try {
            //get sharepoint sites

            // get all sites
            var sites = await graphClient.Sites.GetAsync();
            //requestConfiguration => requestConfiguration.QueryParameters.Select = new string[] { "id", "displayName", "mail" });
            if (sites != null && sites.Value != null) {
                foreach (var site in sites.Value) {
                    if (site == null) continue;
                    Console.WriteLine($"site({site.Id}):Name:{site.Name}:{site.DisplayName}");
                }
                if (sites.Value.Count == 0) { Console.WriteLine("no sites found"); }
            }
            else { Console.WriteLine("no sites found"); }
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
    public static async Task GetSharepointSiteAsync(GraphServiceClient graphClient, string siteid, string driveid) {
        try {
            var site = await graphClient
                .Sites[$"{siteid}"]
                .GetAsync(requestConfiguration => {
                    //requestConfiguration.QueryParameters.Select = new string[] { "id", "createdDateTime", "displayName" };
                    requestConfiguration.QueryParameters.Expand = new string[] { "drives", "lists" };
                });
            if (site != null) {
                Console.WriteLine($"site({site.Id}):Name:{site.Name}:{site.DisplayName}");

                //site.Drives.get
                if (site.Drives != null) {
                    foreach (var drive in site.Drives) {
                        if (drive == null) continue;
                        Console.WriteLine($"  drive({drive.Id}):Name:{drive.Name}:ParentReference({drive.ParentReference})");
                    }
                    var _d = await graphClient
                        .Drives[driveid]
                        .GetAsync();
                        //.GetAsync(requestConfiguration => {
                        //requestConfiguration.QueryParameters.Expand = new string[] { "items" };});//throws oData error
                        //requestConfiguration.QueryParameters.Expand = new string[] { "children" };});//throws oData error
                    if (_d != null) {

                        // display all drive.Items
                        if (_d.Items != null) {
                            foreach (var item in _d.Items) {
                                if (item == null) continue;
                                Console.WriteLine($"    Item({item.Id}):Name:{item.Name}:OdataType({item.OdataType}):folderChildCount:{item.Folder?.ChildCount ?? 0}");
                            }
                        }
                        else { Console.WriteLine($"    no drive({_d.Name}) items found"); }

                        // display all drive.List.Items
                        if (_d.List?.Items != null) {
                            foreach (var item in _d.List.Items) {
                                if (item == null) continue;
                                Console.WriteLine($"    ListItem({item.Id}):Name:{item.Name}:OdataType({item.OdataType})");
                            }
                        }
                        else { Console.WriteLine($"    no drive({_d.Name}) Listitems found"); }
                    }
                    else { Console.WriteLine("    no Drive() found"); }
                }
                else { Console.WriteLine("  no Drives found"); }



                if (site.Lists != null) {
                    foreach (var list in site.Lists) {
                        if (list == null) continue;
                        Console.WriteLine($"  list({list.Id}):Name:{list.Name}:ParentReference({list.ParentReference})");
                        if (list.Items != null) {
                            foreach (var item in list.Items) {
                                if (item == null) continue;
                                Console.WriteLine($"    item({item.Id}):Name:{item.Name}:ParentReference({item.ParentReference})");
                            }
                        }
                    }
                }
                else { Console.WriteLine("  no Lists found"); }

                if (site.Items != null) {
                    foreach (var item in site.Items) {
                        if (item == null) continue;
                        Console.WriteLine($"  item({item.Id}):Name:{item.Name}:OdataType({item.OdataType})");

                    }
                }
                else { Console.WriteLine("no Items found"); }
            }
            else { Console.WriteLine("no sites found"); }
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
    public static async Task CreateNewSubDirectoryAsync(GraphServiceClient graphClient, string siteid, string driveid) {
        //v4 Get the requestInformation to make a POST request
        //var directoryObject = new DirectoryObject() {
        //    Id = Guid.NewGuid().ToString()
        //var requestInformation = graphServiceClient
        //                            .DirectoryObjects
        //                            .ToPostRequestInformation(directoryObject);
        //TODO v5 a new Sub Directory
    }
}

