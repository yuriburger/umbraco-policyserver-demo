using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer.Config
{
    public class MyApiResources
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("application.policy", "My PolicyServerApi")
            };
        }
    }
}
