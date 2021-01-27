using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bouncer.Domain.Interfaces
{
    public interface ICrud<T, TIdType>
    {
        Task<long> AddAsync(T obj);
        void Update(T obj);
        Task<T> FindById(TIdType id, bool asNoTracking = false);
        Task<T> Find(Expression<Func<T, bool>> expression, bool asNoTracking = false);
        Task<IEnumerable<T>> List(Expression<Func<T, bool>> expression);
    }
}
