using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SolIS4.STS.Configuration.UserConfigurator
{
    public class CustomProfileService : IProfileService
    {
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var claims = new List<Claim>
            {
                new Claim("name", context.Subject.GetSubjectId()),
                new Claim("external", "testMiguel"),
                new Claim("admin", "1")
            };
            context.IssuedClaims = claims;
            //return Task.FromResult(0);

        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            //return Task.FromResult(0);

        }
    }
}
