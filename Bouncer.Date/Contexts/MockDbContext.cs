using Bouncer.Domain.Entities.Auth;
using Bouncer.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Bouncer.Data.Contexts
{
    public class MockDbContext : BaseContext, IDbContext
    {
        public virtual DbSet<UserApp> tblUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase("BouncerDB");
            options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            base.OnConfiguring(options);
        }
    }
}
