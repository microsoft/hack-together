using Microsoft.Kiota.Abstractions.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSGraphAuth {
    public class TokenProvider : IAccessTokenProvider {
        public TokenProvider() {
        }

        public Task<string> GetAuthorizationTokenAsync(Uri uri, Dictionary<string, object>? additionalAuthenticationContext = default,
       CancellationToken cancellationToken = default) {
            var token = "token";
            // get the token and return it in your own way
            return Task.FromResult(token);
        }

        public AllowedHostsValidator? AllowedHostsValidator { get; }
    }
}
