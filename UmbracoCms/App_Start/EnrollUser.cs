using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models.Membership;

namespace UmbracoCms
{
    public class EnrollUser
    {
        public static async Task GenerateIdentityAsync(
            SecurityTokenValidatedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> notification)
        {
            var identityUser = new ClaimsIdentity(
                notification.AuthenticationTicket.Identity.Claims,
                notification.AuthenticationTicket.Identity.AuthenticationType,
                ClaimTypes.Name,
                ClaimTypes.Role);

            // We need this for updating the user role in Umbraco BackOffice
            var userId = notification.AuthenticationTicket.Identity.FindFirst(ClaimTypes.NameIdentifier);

            // Call PolicyServer API
            var policyClient = new HttpClient();
            policyClient.SetBearerToken(notification.ProtocolMessage.AccessToken);

            // Get the Roles
            var response = await policyClient.GetAsync(new Uri("http://localhost:5001/api/policies"));
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var roles = JObject.Parse(content)["value"];

                // Pass roles result from PolicyServer
                if (roles != null)
                    RegisterUserWithUmbracoRole(userId.Value, roles, notification.Options.Authority);
            }

            notification.AuthenticationTicket = new AuthenticationTicket(identityUser,
                notification.AuthenticationTicket.Properties);
        }

        private static void RegisterUserWithUmbracoRole(string userid, JToken roles, string providerName)
        {
            var userService = ApplicationContext.Current.Services.UserService;
            var user = ApplicationContext.Current.Services.ExternalLoginService.Find(
                            new UserLoginInfo(providerName, userid)).FirstOrDefault();

            if (user == null)
                return;

            var umbracoUser = userService.GetUserById(user.UserId);

            if (umbracoUser == null)
                return;

            // If we find an administrator we need to update the Umbraco Role
            var roleObject = roles.FirstOrDefault(r => r["value"] != null && r["value"].ToString() == "administrator");

            if (roleObject == null)
                return;

            // Add User to Admin Group
            var userGroup = ToReadOnlyGroup(userService.GetUserGroupByAlias("admin"));

            if (userGroup == null)
                return;

            umbracoUser.AddGroup(userGroup);
            userService.Save(umbracoUser);
        }

        public static IReadOnlyUserGroup ToReadOnlyGroup(IUserGroup group)
        {
            // This will generally always be the case
            if (group is IReadOnlyUserGroup readonlyGroup) return readonlyGroup;

            // otherwise create one
            return new ReadOnlyUserGroup(
                group.Id,
                group.Name,
                group.Icon,
                group.StartContentId,
                group.StartMediaId,
                group.Alias,
                group.AllowedSections,
                group.Permissions);
        }
    }
}