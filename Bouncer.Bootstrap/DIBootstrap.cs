using Bouncer.Data.Contexts;
using Bouncer.DI;
using Bouncer.Domain.Entities.Auth;
using Bouncer.Mapping;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Identity;
using Bouncer.Bootstrap;

namespace Bouncer.Bootstrap
{
    public static class DIBootstrap
    {
        public static void RegisterAppTypes(IServiceCollection service)
        {
            service.RegisterAppServices()
                .RegisterAppBusiness()
                .RegisterAppPersistence();

            AppContainer.SetContainer(service);
            AutoMapperConfiguration.Register();

            Migrate(service);
        }
        
        public static void RegisterAuthTypes(IServiceCollection service)
        {
            service.RegisterAuthServices()
                .RegisterAuthBusiness()
                .RegisterAuthPersistence();

            service.AddIdentity<UserApp, Role>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1d);
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();

            AppContainer.SetContainer(service);
            AutoMapperConfiguration.Register();

            Migrate(service);
        }

        public static void MockRegisterTypes(IServiceCollection service)
        {
            service.RegisterAppServices()
                .RegisterAppBusiness()
                .MockRegisterAppPersistence();

            AppContainer.SetContainer(service);
            AutoMapperConfiguration.Register();

            service.BuildServiceProvider().GetService<MockDbContext>().Database.EnsureCreated();
        }

        private static void Migrate(IServiceCollection services)
        {
            var dao = services.BuildServiceProvider().GetService<AuthDbContext>();
            dao.Database.EnsureCreated();
            //dao.Database.Migrate();
        }
    }
}