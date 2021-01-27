using Bouncer.Application.AppServices;
using Bouncer.Data.Contexts;
using Bouncer.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Bouncer.Domain.Entities.Auth;
using Bouncer.Domain.Entities;
using Bouncer.Application;
using Bouncer.Business.App;

namespace Bouncer.Bootstrap
{
    public static class BigContainer
    {
        public static IServiceCollection RegisterAuthServices(this IServiceCollection service)
        {
            service.AddTransient<UserAppService>();
            service.AddTransient<LoginAppService>();
            service.AddTransient<UserManager<UserApp>>();

            return service;
        }

        public static IServiceCollection RegisterAuthBusiness(this IServiceCollection service)
        {
            return service;
        }
        
        public static IServiceCollection RegisterAuthPersistence(this IServiceCollection service)
        {
            service.AddDbContext<AuthDbContext>();
            return service;
        }

        public static IServiceCollection RegisterAppServices(this IServiceCollection service)
        {
            service.AddTransient<ShiftAppService>();
            
            return service;
        }

        public static IServiceCollection RegisterAppBusiness(this IServiceCollection service)
        {
            service.AddTransient<IBusiness<Shift>, ShiftBusiness>();
            //service.AddTransient<IBusiness<BankAccount>, BankAccountBusiness>();
            //service.AddTransient<IBusiness<Payer>, PayerBusiness>();
            //service.AddTransient<IBusiness<Debit>, DebitBusiness>();
            //service.AddTransient<IBusiness<Bill>, BillBusiness>();
            //service.AddTransient<IBusiness<Payment>, PaymentBusiness>();

            return service;
        }
        
        public static IServiceCollection RegisterAppPersistence(this IServiceCollection service)
        {
            service.AddDbContext<AuthDbContext>();
            service.AddTransient<IDbContext, SqlServeDbContext>();
            return service;
        }


        public static IServiceCollection MockRegisterAppPersistence(this IServiceCollection service)
        {
            service.AddDbContext<MockDbContext>();
            service.AddTransient<IDbContext, MockDbContext>();
            return service;
        }
    }
}
