using AutoMapper;
using SampleAppWithDapper.DataAccess;
using SampleAppWithDapper.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleAppWithDapper.ServicePattern
{
    public class GenericServiceAsync<Tv, Te> : IServiceAsync<Tv, Te> where Tv : BaseDomain
        where Te : BaseRepository
    {
        protected IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenericServiceAsync(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public GenericServiceAsync()
        {
        }

        public virtual async Task<IEnumerable<Tv>> GetAll()
        {
            var entities = await _unitOfWork.GetRepositoryAsync<Te>().GetAll();
            return _mapper.Map<IEnumerable<Tv>>(source: entities);
        }

        public virtual async Task<Tv> GetOne(int id)
        {
            var entity = await _unitOfWork.GetRepositoryAsync<Te>().GetOne(id);
            return _mapper.Map<Tv>(source: entity);
        }

        public virtual async Task<int> Add(Tv view)
        {
            var entity = _mapper.Map<Te>(source: view);
            int id = await _unitOfWork.GetRepositoryAsync<Te>().Insert(entity);
            await _unitOfWork.SaveAsync();
            return id;
        }

        public async Task<bool> Update(Tv view)
        {
            await _unitOfWork.GetRepositoryAsync<Te>().UpdateAsync(view.Id, _mapper.Map<Te>(source: view));
            return await _unitOfWork.SaveAsync();
        }

        public virtual async Task<bool> Remove(int id)
        {
            await _unitOfWork.GetRepositoryAsync<Te>().Delete(id);
            return await _unitOfWork.SaveAsync();
        }

        public virtual async Task<IEnumerable<Tv>> Get(Func<Te, bool> predicate)
        {
            var items = await _unitOfWork.GetRepositoryAsync<Te>().Get(predicate as Func<object, bool>);
            return _mapper.Map<IEnumerable<Tv>>(source: items);
        }
    }
}