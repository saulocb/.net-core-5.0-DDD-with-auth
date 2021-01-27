using Bouncer.Common.InternalObjects;
using System;

namespace Bouncer.ViewModels
{
    public class EntityBase_vw<T> : IEntity<T>
    {
        public T Id { get; set; }
    }
}
