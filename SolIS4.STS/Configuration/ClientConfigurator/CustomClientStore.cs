using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
namespace SolIS4.STS.Configuration.ClientConfigurator
{
    public class CustomClientStore : IClientStore
    {
        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            /*
            return new Client
            {
                Claims = new List<ClientClaim> { new ClientClaim { Type = "role", Value = "admin,support"} },
                ClientId = "company-employee",
                ClientSecrets = new[] { new Secret("codemazesecret".Sha512()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, "companyApi" }
            };
            */
            return new Client
            {
                ClientName = "Angular-Client",
                ClientId = "angular-client",
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = new List<string> { "http://localhost:4200/signin-callback", "http://localhost:4200/assets/silent-callback.html" },
                RequirePkce = true,
                AllowAccessTokensViaBrowser = true,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "companyApi",
                },
                AllowedCorsOrigins = { "http://localhost:4200" },
                RequireClientSecret = false,
                //ClientSecrets = { new Secret("MVCSecret".Sha512()) },
                PostLogoutRedirectUris = new List<string> { "http://localhost:4200/signout-callback" },
                RequireConsent = false,
                AccessTokenLifetime = 600,
            };
        }
    }
}
