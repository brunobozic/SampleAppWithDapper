using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleAppWithDapper.ServicePattern
{
    public interface IServiceAsync<Tv, Te>
    {
        Task<IEnumerable<Tv>> GetAll();
        Task<int> Add(Tv obj);
        Task<bool> Update(Tv obj);
        Task<bool> Remove(int id);
        Task<Tv> GetOne(int id);
        Task<IEnumerable<Tv>> Get(Func<Te, bool> predicate);
    }
}