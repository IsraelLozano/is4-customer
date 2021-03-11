using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolIS4.STS.Configuration.UserConfigurator;

namespace SolIS4.STS.Configuration
{
    public static class IdentisyServerExtensions
    {

        public static IIdentityServerBuilder AddCustomUserStore(this IIdentityServerBuilder builder)
        {
            builder.AddProfileService<CustomProfileService>();
            //builder.AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>();

            return builder;
        }
    }
}
