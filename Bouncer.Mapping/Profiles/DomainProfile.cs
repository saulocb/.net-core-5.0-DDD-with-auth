using AutoMapper;
using Bouncer.Domain;
using Bouncer.Domain.Entities;
using Bouncer.Domain.Entities.Auth;
using Bouncer.ViewModels;
using Bouncer.ViewModels.AppObject;
using Bouncer.ViewModels.AppObjects;

namespace Bouncer.Mapping.Profiles
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<EntityBase<long>, EntityBase_vw<long>>().ReverseMap();
            CreateMap<UserApp, UserApp_vw>().ReverseMap();
            CreateMap<Shift, Shift_vw>().ReverseMap();
        }
    }
}
