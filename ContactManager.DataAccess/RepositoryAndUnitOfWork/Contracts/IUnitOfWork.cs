using System;
using System.Threading.Tasks;

namespace SampleAppWithDapper.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class;

        DatabaseConnectionManager Context { get; }

        bool Save();

        Task<bool> SaveAsync();
    }
}