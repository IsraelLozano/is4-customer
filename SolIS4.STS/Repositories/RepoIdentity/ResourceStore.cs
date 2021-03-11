using dic.sso.identityserver.oauth.Repositories;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SolIS4.STS.Repositories.RepoIdentity
{
    public class ResourceStore : Repository, IResourceStore
    {
        public ResourceStore(Func<IDbConnection> openConnection) : base(openConnection)
        {
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {

            var lista = new List<ApiResource>()
            {
                new ApiResource("companyApi", "CompanyEmployee API")
                {
                    Scopes = { "companyApi" }
                }
            };

            return Task.FromResult(lista.AsEnumerable());


        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var lista = new List<ApiResource>()
            {
                new ApiResource("companyApi", "CompanyEmployee API")
                {
                    Scopes = { "companyApi" }
                }
            };

            return Task.FromResult(lista.AsEnumerable());

        }

        public Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            var lista = new List<ApiScope> { new ApiScope("companyApi", "CompanyEmployee API") };
            return  Task.FromResult(lista.AsEnumerable());

        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var lista = new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Address(),
                new IdentityResource("roles", "User role(s)", new List<string> { "role" }),
                new IdentityResource("position", "Your position", new List<string> { "position" }),
                new IdentityResource("country", "Your country", new List<string> { "country" })
            };

            return  Task.FromResult(lista.AsEnumerable());
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            var identityResources = new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Address(),
                new IdentityResource("roles", "User role(s)", new List<string> { "role" }),
                new IdentityResource("position", "Your position", new List<string> { "position" }),
                new IdentityResource("country", "Your country", new List<string> { "country" })
            };

            var apiResources = new List<ApiResource>()
            {
                new ApiResource("companyApi", "CompanyEmployee API")
                {
                    Scopes = { "companyApi" }
                }
            };

            var lista = new List<ApiScope> { new ApiScope("companyApi", "CompanyEmployee API") };


            Resources result = new Resources(identityResources.AsEnumerable(), apiResources.AsEnumerable(), lista.AsEnumerable());
            return Task.FromResult(result);

        }
    }
}
