using System;
using System.Collections.Generic;

namespace SampleAppWithDapper.ServicePattern
{
    public interface IService<Tv, Te>
    {
        IEnumerable<Tv> GetAll();

        int Add(Tv obj);

        bool Update(Tv obj);

        bool Remove(int id);

        Tv GetOne(int id);

        IEnumerable<Tv> Get(Func<Te, bool> predicate);
    }
}