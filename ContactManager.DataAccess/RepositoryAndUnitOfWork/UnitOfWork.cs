using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleAppWithDapper.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        public DatabaseConnectionManager Context { get; }

        private Dictionary<Type, object> _repositoriesAsync;
        private Dictionary<Type, object> _repositories;
        private bool _disposed;

        public UnitOfWork(DatabaseConnectionManager context)
        {
            Context = context;
            _disposed = false;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
                switch (type.ToString())
                {
                    //case "ApiNCoreDapper.Entity.Account":
                    //    _repositories[type] = new AccountRepository(Context);
                    //    break;
                    //case "ApiNCoreDapper.Entity.User":
                    //    _repositories[type] = new UserRepository(Context);
                    //    break;
                }
            return (IRepository<TEntity>)_repositories[type];
        }

        public IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class
        {
            if (_repositoriesAsync == null) _repositoriesAsync = new Dictionary<Type, object>();
            var type = typeof(TEntity);
            if (!_repositoriesAsync.ContainsKey(type))
                switch (type.ToString())
                {
                    //case "ApiNCoreDapper.Entity.Account":
                    //    _repositoriesAsync[type] = new AccountRepositoryAsync(Context);
                    //    break;
                    //case "ApiNCoreDapper.Entity.User":
                    //    _repositoriesAsync[type] = new UserRepositoryAsync(Context);
                    //    break;
                }
            return (IRepositoryAsync<TEntity>)_repositoriesAsync[type];
        }

        public bool Save()
        {
            Context.Transaction?.Commit();
            return true;
        }

        public async Task<bool> SaveAsync()
        {
            await Task.Run(() =>
            {
                Context.Transaction?.Commit();
            });
            return true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool isDisposing)
        {
            if (!_disposed)
            {
                if (isDisposing)
                {
                    Context.Connection?.Dispose();
                    Context.Transaction?.Dispose();
                }
            }
            _disposed = true;
        }
    }
}