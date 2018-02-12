using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServer.Config
{
    public class MyClients
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "u-client-bo",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256()),
                    },
                    ClientName = "Umbraco Client",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RequireConsent = false,
                    RedirectUris           = { "http://localhost:5003/umbraco" },
                    PostLogoutRedirectUris = { "http://localhost:5003/umbraco" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "application.profile",
                        "application.policy"
                    },
                    AllowAccessTokensViaBrowser = true,
                    AlwaysIncludeUserClaimsInIdToken = true
                }
            };
        }

    }
}
