using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//using SampleAppWithDapper.Domain.DomainModels.Aircraft;

namespace SampleAppWithDapper.DataAccess
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        //Task<IEnumerable<Aircraft>> Get(Func<T, bool> predicate);
        Task<T> GetOne(object id);
        Task<int> Insert(T entity);
        Task Delete(T entity);
        Task Delete(object id);
        Task SoftDelete(T entity);
        Task SoftDelete(object id);
        Task UpdateAsync(object id, T entity);
        Task<object> Get(Func<object,bool> predicate);
    }
}
