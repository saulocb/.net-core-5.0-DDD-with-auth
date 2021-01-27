using Bouncer.Common.InternalObjects;
using Bouncer.Domain.Interfaces;

namespace Bouncer.Application
{
    public interface IBusiness<T> : ICrud<T, long> where T : IEntity<long>
    {
    }
}
