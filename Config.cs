using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServerProject
{
    public class Config
    {
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
             {
                 new ApiScope(name: "apiscope", displayName: "Api Scope.")
             };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                 new Client
                {
                    ClientId = "secret_user_client_id",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "apiscope" }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
        {
        new TestUser
        {
            SubjectId = "1",
            Username = "user",
            Password = "user",
            Claims = new[]
            {
                new Claim("roleType", "CanReaddata")
            }
        },
        new TestUser
        {
            SubjectId = "2",
            Username = "admin",
            Password = "admin",
            Claims = new[]
            {
                new Claim("roleType", "CanUpdatedata")
            }
        }
        };
        }
    }
}
