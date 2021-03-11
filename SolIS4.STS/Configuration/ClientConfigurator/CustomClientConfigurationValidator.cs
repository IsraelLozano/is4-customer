using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolIS4.STS.Configuration.ClientConfigurator
{
    public class CustomClientConfigurationValidator : IClientConfigurationValidator
    {
        public async Task ValidateAsync(ClientConfigurationValidationContext context)
        {
            string test;

            test = "Hola Mundo";
            //throw new NotImplementedException();
        }
    }
}
