using Microsoft.Owin;
using Owin;
using Umbraco.Core;
using Umbraco.Core.Security;
using Umbraco.Web.Security.Identity;
using Umbraco.IdentityExtensions;
using UmbracoCms;
using Microsoft.Owin.Security.OpenIdConnect;

//To use this startup class, change the appSetting value in the web.config called 
// "owin:appStartup" to be "UmbracoCustomOwinStartup"

[assembly: OwinStartup("UmbracoCustomOwinStartup", typeof(UmbracoCustomOwinStartup))]

namespace UmbracoCms
{
    /// <summary>
    /// A custom way to configure OWIN for Umbraco
    /// </summary>
    /// <remarks>
    /// The startup type is specified in appSettings under owin:appStartup - change it to "UmbracoCustomOwinStartup" to use this class
    /// 
    /// This startup class would allow you to customize the Identity IUserStore and/or IUserManager for the Umbraco Backoffice
    /// </remarks>
    public class UmbracoCustomOwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            //Configure the Identity user manager for use with Umbraco Back office

            // *** EXPERT: There are several overloads of this method that allow you to specify a custom UserStore or even a custom UserManager!            
            app.ConfigureUserManagerForUmbracoBackOffice(
                ApplicationContext.Current,
				//The Umbraco membership provider needs to be specified in order to maintain backwards compatibility with the 
				// user password formats. The membership provider is not used for authentication, if you require custom logic
				// to validate the username/password against an external data source you can create create a custom UserManager
				// and override CheckPasswordAsync
                global::Umbraco.Core.Security.MembershipProviderExtensions.GetUsersMembershipProvider().AsUmbracoMembershipProvider());
            
            //Ensure owin is configured for Umbraco back office authentication
            app
                .UseUmbracoBackOfficeCookieAuthentication(ApplicationContext.Current)
                .UseUmbracoBackOfficeExternalCookieAuthentication(ApplicationContext.Current);

            var identityOptions = new OpenIdConnectAuthenticationOptions
            {
                ClientId = "u-client-bo",
                SignInAsAuthenticationType = Constants.Security.BackOfficeExternalAuthenticationType,
                Authority = "http://localhost:5000",
                RedirectUri = "http://localhost:5003/umbraco",
                PostLogoutRedirectUri = "http://localhost:5003/umbraco",
                ResponseType = "code id_token token",
                Scope = "openid profile email application.profile application.policy"
            };

            // Configure BackOffice Account Link button and style
            identityOptions.ForUmbracoBackOffice("btn-microsoft", "fa-windows");
            identityOptions.Caption = "OpenId Connect";

            // Fix Authentication Type
            identityOptions.AuthenticationType = "http://localhost:5000";

            // Configure AutoLinking
            identityOptions.SetExternalSignInAutoLinkOptions(new ExternalSignInAutoLinkOptions(
                autoLinkExternalAccount: true,
                defaultUserGroups: null,
                defaultCulture: null
                ));

            identityOptions.Notifications = new OpenIdConnectAuthenticationNotifications
            {
                SecurityTokenValidated = EnrollUser.GenerateIdentityAsync
            };

            app.UseOpenIdConnectAuthentication(identityOptions);
        }
    }
}
