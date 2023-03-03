using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharepoint.IO {

    public interface ISharepointHelperService {
        Task<Microsoft.Graph.Models.Site?> GetSiteAsync(string siteid);
    }

    public class SharepointHelperService : ISharepointHelperService {
        private readonly GraphServiceClient graphClient;

        public SharepointHelperService(GraphServiceClient graphClient) {
            this.graphClient = graphClient;
        }

        /// <summary>
        /// https://github.com/microsoftgraph/msgraph-sdk-dotnet/blob/7a2be45d2cf37f18a32cc9a60d0edf441fd23a08/docs/v4-reference-docs/site-get.md
        /// </summary>
        /// <param name="siteid"></param>
        /// <returns><see cref="Microsoft.Graph.Models.Site"/></returns>
        public async Task<Microsoft.Graph.Models.Site?> GetSiteAsync(string siteid) {
            var site = await graphClient.Sites[$"{siteid}"]
            .GetAsync();
            return site?.Sites?.First();
        }
    }

}
