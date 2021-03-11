using Dapper;
using dic.sso.identityserver.oauth.Repositories;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SolIS4.STS.Repositories.RepoIdentity
{
    public class ClientStore : Repository, IClientStore
    {
        //public UserRepository(Func<IDbConnection> openConnection) : base(openConnection) { }
        public ClientStore(Func<IDbConnection> openConnection) : base(openConnection)
        {

        }
        public async Task<Client> FindClientByIdAsync(string clientId)
        {

            int idAPlicacion = 27;

            using (var connection = OpenConnection())
            {
                //var queryResult = await connection.QueryAsync<User>("select * from [Users] where [Username]=@username and [Password]=@password", 
                var queryResult =
                    await connection.QueryAsync<SegSSOMAplicacion>("SELECT * from SegSSOMAplicacion WHERE PK_eIdAplicacion = @idAplicacion", new { idAPlicacion });

                var resultado = queryResult.SingleOrDefault();

                var cliente = new Client
                {
                    ClientName = resultado.uNombreAplicacion,
                    ClientId = resultado.PK_eIdAplicacion.ToString(),
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = new List<string> { "http://localhost:4200/signin-callback", "http://localhost:4200/assets/silent-callback.html" },
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "companyApi"
                    },
                    AllowedCorsOrigins = { "http://localhost:4200" },
                    RequireClientSecret = false,
                    //ClientSecrets = { new Secret("MVCSecret".Sha512()) },
                    PostLogoutRedirectUris = new List<string> { "http://localhost:4200/signout-callback" },
                    RequireConsent = false,
                    AccessTokenLifetime = 600,
                };

                return cliente;
            }
        }
    }
}
