using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolIS4.STS.Configuration.ClientConfigurator
{
    public class CustomResourceStore : IResourceStore
    {
        public Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            //paso 2

            List<ApiResource> lista = new List<ApiResource> {

                new ApiResource("companyApi", "CompanyEmployee API")
                {
                    Scopes = { "companyApi" },
                    UserClaims = new[] {"email" }
                }

            };

            return lista;
        }

        public async Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            //paso 3

            var lista = new List<ApiScope>
            {
                new ApiScope("companyApi", "CompanyEmployee API")
            };

            return lista;
        }

        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            //paso 1
            //reemplaza a .AddInMemoryApiScopes(InMemoryConfiguration.GetApiScopes())

            List<IdentityResource> resources = new List<IdentityResource>() {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Address(),
                new IdentityResource("roles", "User role(s)", new List<string> { "role" }),
                new IdentityResource("position", "Your position", new List<string> { "position" }),
                new IdentityResource("country", "Your country", new List<string> { "country" })
            };

            return resources;
        }

        public async Task<Resources> GetAllResourcesAsync()
        {
            //todos juntos
            List<IdentityResource> resources = new List<IdentityResource>() {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Address(),
                new IdentityResource("roles", "User role(s)", new List<string> { "role" }),
                new IdentityResource("position", "Your position", new List<string> { "position" }),
                new IdentityResource("country", "Your country", new List<string> { "country" })
            };


            var listaScope = new List<ApiScope>
            {
                new ApiScope("companyApi", "CompanyEmployee API")
            };

            List<ApiResource> listaResources = new List<ApiResource> {

                new ApiResource("companyApi", "CompanyEmployee API")
                {
                    Scopes = { "companyApi" },
                    UserClaims = new[] {"email" }
                }

            };

            var result = new Resources(
                resources, listaResources, listaScope);

            return result;
            /*
            return new List<Resource>() {
                new ApiResource("companyApi", "CompanyEmployee API")
                {
                    Scopes = { "companyApi" },
                    UserClaims = new[] {"email" }
                }
            };
            */
            //throw new NotImplementedException();
        }
    }
}
