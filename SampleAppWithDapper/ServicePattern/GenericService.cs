using System;
using System.Collections.Generic;
using AutoMapper;
using SampleAppWithDapper.DataAccess;
using SampleAppWithDapper.Domain;

namespace SampleAppWithDapper.ServicePattern
{
    public class GenericService<Tv, Te> : IService<Tv, Te> where Tv : BaseDomain
        where Te : BaseEntity
    {

        protected IUnitOfWork _unitOfWork;
        public GenericService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public GenericService()
        {
        }

        public virtual IEnumerable<Tv> GetAll()
        {
            var entities = _unitOfWork.GetRepository<Te>().GetAll();
            return Mapper.Map<IEnumerable<Tv>>(source: entities);
        }
        public virtual Tv GetOne(int id)
        {
            var entity = _unitOfWork.GetRepository<Te>().GetOne(id);
            return Mapper.Map<Tv>(source: entity);
        }

        public virtual int Add(Tv view)
        {
            var entity = Mapper.Map<Te>(source: view);
            int id = _unitOfWork.GetRepository<Te>().Insert(entity);
            _unitOfWork.Save();
            return id;
        }

        public virtual bool Update(Tv view)
        {
            _unitOfWork.GetRepository<Te>().Update(view.Id, Mapper.Map<Te>(source: view));
            return _unitOfWork.Save();
        }


        public virtual bool Remove(int id)
        {
            _unitOfWork.GetRepository<Te>().Delete(id);
            return _unitOfWork.Save();
        }

        public virtual IEnumerable<Tv> Get(Func<Te, bool> predicate)
        {
            var entities = _unitOfWork.GetRepository<Te>().Get(predicate);
            return Mapper.Map<IEnumerable<Tv>>(source: entities);
        }
    }
}