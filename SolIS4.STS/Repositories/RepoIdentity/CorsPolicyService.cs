using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SolIS4.STS.Repositories.RepoIdentity
{
    public class CorsPolicyService : ICorsPolicyService, IRepository
    {
        private readonly string[] _allowedOrigins;

        public CorsPolicyService(IRepository repository)
        {
            _allowedOrigins = repository.All<Client>().SelectMany(x => x.AllowedCorsOrigins).ToArray();
        }
        public Task<bool> IsOriginAllowedAsync(string origin) => Task.FromResult(_allowedOrigins.Contains(origin));

        public void Add<T>(T item) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public void Add<T>(IEnumerable<T> items) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> All<T>() where T : class, new()
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            throw new NotImplementedException();
        }


        public T Single<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Where<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            throw new NotImplementedException();
        }
    }


    public interface IRepository
    {
        IQueryable<T> All<T>() where T : class, new();
        IQueryable<T> Where<T>(Expression<Func<T, bool>> expression) where T : class, new();
        T Single<T>(Expression<Func<T, bool>> expression) where T : class, new();
        void Delete<T>(Expression<Func<T, bool>> expression) where T : class, new();
        void Add<T>(T item) where T : class, new();
        void Add<T>(IEnumerable<T> items) where T : class, new();
    }

}
