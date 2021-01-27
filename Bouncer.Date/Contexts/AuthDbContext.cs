using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Bouncer.Domain.Entities.Auth;
using Bouncer.DI;
using OxiFin.Data.Configuration;

namespace Bouncer.Data.Contexts
{
    public class AuthDbContext : IdentityDbContext<UserApp, Role, long, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        protected readonly IConfiguration _configuration;
        protected readonly ILoggerFactory _loggerFactory;
        protected readonly ILogger _log;
         
        public AuthDbContext()
        {
            _loggerFactory = AppContainer.Resolve<ILoggerFactory>();
            _configuration = AppContainer.Resolve<IConfiguration>();

            _log = _loggerFactory.CreateLogger<BaseContext>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            
            options.UseSqlServer(_configuration.GetConnectionString("BouncerAuthDB"));
            base.OnConfiguring(options);

            options.UseLoggerFactory(_loggerFactory);

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
