using AutoMapper;
using SampleAppWithDapper.DataAccess;
using SampleAppWithDapper.Domain;
using System;
using System.Collections.Generic;

namespace SampleAppWithDapper.ServicePattern
{
    public class GenericService<Tv, Te> : IService<Tv, Te> where Tv : BaseDomain
        where Te : BaseRepository
    {
        protected IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenericService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public GenericService()
        {
        }

        public virtual IEnumerable<Tv> GetAll()
        {
            var entities = _unitOfWork.GetRepository<Te>().GetAll();
            return _mapper.Map<IEnumerable<Tv>>(source: entities);
        }

        public virtual Tv GetOne(int id)
        {
            var entity = _unitOfWork.GetRepository<Te>().GetOne(id);
            return _mapper.Map<Tv>(source: entity);
        }

        public virtual int Add(Tv view)
        {
            var entity = _mapper.Map<Te>(source: view);
            int id = _unitOfWork.GetRepository<Te>().Insert(entity);
            _unitOfWork.Save();
            return id;
        }

        public virtual bool Update(Tv view)
        {
            _unitOfWork.GetRepository<Te>().Update(view.Id, _mapper.Map<Te>(source: view));
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
            return _mapper.Map<IEnumerable<Tv>>(source: entities);
        }
    }
}