using dic.sso.identityserver.oauth.Models;
using dic.sso.identityserver.oauth.Repositories;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Provias.Seguridad.Utiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SolIS4.STS.Configuration.UserConfigurator
{
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {

        private IUserRepository userRepository;

        public CustomResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {

            var user = await userRepository.GetAsync(context.UserName, EncriptacionHelper.EncryptToByte(context.Password));
            if (user != null)
            {
                context.Result = new GrantValidationResult(user.PkId.ToString(), authenticationMethod: "custom", claims: GetUserClaims(user));
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid Credentials");

            }
            //return Task.FromResult(context.Result);
        }

        public static Claim[] GetUserClaims(User user)
        {
            return new Claim[]
            {
            new Claim("user_id", user.PkId.ToString() ?? ""),
            new Claim(JwtClaimTypes.Name, user.Username),
            new Claim(JwtClaimTypes.GivenName, user.Username),
            new Claim(JwtClaimTypes.FamilyName, user.Username ?? ""),
            new Claim(JwtClaimTypes.Email, user.Email),
            //roles
             new Claim(JwtClaimTypes.Role, "Administrator")
            };
        }

    }


    //public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    //{

    //    if (context.UserName == "jperez" && context.Password == "123456")
    //    {
    //        context.Result = new GrantValidationResult(context.UserName, OidcConstants.AuthenticationMethods.Password);
    //    }
    //    else
    //    {
    //        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid Credentials");

    //    }
    //}
}
