using Bouncer.Common.Exceptions;
using Bouncer.Common.InternalObjects;
using Bouncer.Mapping;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bouncer.Application
{
    public class BaseAppService<T_vw, T>
        where T_vw : IEntity<long>
        where T : IEntity<long>
    {
        protected readonly IBusiness<T> _baseBusiness;

        public BaseAppService(IBusiness<T> business)
        {
            _baseBusiness = business;
        }

        public virtual async Task<AppResult> AddAsync(T_vw obj)
        {
            return new AppResult(await _baseBusiness.AddAsync(Resolve(obj)));
        }

        public virtual void Update(T_vw obj)
        {
            _baseBusiness.Update(Resolve(obj));
        }

        public virtual async Task<AppResult<T_vw>> FindByIdAsync(long id)
        {
            var result = await _baseBusiness.FindById(id, true);
            
            if (result == null)
                return new AppResult<T_vw>("Not found");

            return new AppResult<T_vw>(Resolve(result));
        }

        #region resolver
        protected T_vw Resolve(T entity)
        {
            if (entity == null)
                throw new InvalidObjectException();

            return MappingWraper.Map<T, T_vw>(entity);
        }
        protected T Resolve(T_vw viewModel)
        {
            if (viewModel == null)
                throw new InvalidObjectException();

            return MappingWraper.Map<T_vw, T>(viewModel);
        }

        protected TTo Resolve<TFrom, TTo>(TFrom entity) 
            where TFrom : IEntity<long> 
            where TTo : IEntity<long>
        {
            if (entity == null)
                throw new InvalidObjectException();

            return MappingWraper.Map<TFrom, TTo>(entity);
        }

        protected IEnumerable<T_vw> Resolve(IEnumerable<T> entity)
        {
            if (entity == null)
                throw new InvalidObjectException();

            return MappingWraper.Map<IEnumerable<T>, IEnumerable<T_vw>>(entity);
        }
        protected IEnumerable<T> Resolve(IEnumerable<T_vw> viewModel)
        {
            if (viewModel == null)
                throw new InvalidObjectException();

            return MappingWraper.Map<IEnumerable<T_vw>, IEnumerable<T>>(viewModel);
        }
        #endregion
    }
}
