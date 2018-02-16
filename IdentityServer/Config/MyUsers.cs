using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer.Config
{
    public class MyUsers
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                // Umbraco Autolinking depends on the following well-known claim types to be present: 
                // ClaimTypes.NameIdentifier http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier
                // ClaimTypes.Email http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress
                // ClaimTypes.GivenName http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname
                // ClaimTypes.Surname http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname

                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, "Alice Smith"),
                        new Claim(ClaimTypes.GivenName, "Alice"),
                        new Claim(ClaimTypes.Surname, "Smith"),
                        new Claim(ClaimTypes.Email, "AliceSmith@email.com")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, "Bob Roberts"),
                        new Claim(ClaimTypes.GivenName, "Bob"),
                        new Claim(ClaimTypes.Surname, "Roberts"),
                        new Claim(ClaimTypes.Email, "BobRoberts@email.com")
                    }
                },
                new TestUser
                {
                    SubjectId = "3",
                    Username = "jessica",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, "Jessica Roberts"),
                        new Claim(ClaimTypes.GivenName, "Jessica"),
                        new Claim(ClaimTypes.Surname, "Roberts"),
                        new Claim(ClaimTypes.Email, "JessicaRoberts@email.com")
                    }
                }
            };
        }
    }
}
