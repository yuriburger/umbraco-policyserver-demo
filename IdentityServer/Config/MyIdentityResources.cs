using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer.Config
{
    public class MyIdentityResources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            var customProfile = new IdentityResource(
                name: "application.profile",
                displayName: "Application profile",
                claimTypes: new[] { ClaimTypes.GivenName, ClaimTypes.Surname, ClaimTypes.Email, ClaimTypes.Name }
            );

            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                customProfile
            };
        }
    }
}
