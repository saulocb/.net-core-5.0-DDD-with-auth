
using Bouncer.Common.InternalObjects;
using Bouncer.DI;
using Bouncer.Domain.Interfaces;

namespace Bouncer.Business
{
    public abstract class AuthBusiness<TEntity> where TEntity : IEntity<long>
    {
        protected readonly IDbContext _uow;
        
        public AuthBusiness()
        {
            _uow = AppContainer.Resolve<IDbContext>();            
        }
    }
}