using dic.sso.identityserver.oauth.Models;
using dic.sso.identityserver.oauth.Repositories;
using Provias.Seguridad.Utiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dic.sso.identityserver.oauth.Configuration
{
    public class UserValidator : IUserValidator
    {
        private IUserRepository repository;

        public UserValidator(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<User> AutoProvisionUserAsync(string provider, string userId,
            IEnumerable<Claim> claims)
        {
            var user = new User();
            
            await repository.AddAsync(user);

            return user;
        }

        public Task<User> FindByExternalProviderAsync(string provider, string userId)
        {
            return FindByUsernameAsync(userId);
        }

        public Task<User> FindByUsernameAsync(string username)
        {
            return repository.GetAsync(username);
        }

        public async Task<bool> ValidateCredentialsAsync(string username, byte[] password)
        {
            var user = await repository.GetAsync(username, EncriptacionHelper.EncryptToByte(password));

            return user != null;
        }

    }

    public interface IUserValidator
    {
        Task<bool> ValidateCredentialsAsync(string username, byte[] password);
        Task<User> FindByUsernameAsync(string username);
        Task<User> FindByExternalProviderAsync(string provider, string userId);
        Task<User> AutoProvisionUserAsync(string provider, string userId, IEnumerable<Claim> claims);

    }

}
