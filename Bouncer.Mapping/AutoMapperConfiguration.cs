using AutoMapper;
using Bouncer.Mapping.Profiles;

namespace Bouncer.Mapping
{
    public static class AutoMapperConfiguration
    {
        public static MapperConfiguration _config;

        public static void Register()
        {
            _config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainProfile());
            });
        }
    }
}