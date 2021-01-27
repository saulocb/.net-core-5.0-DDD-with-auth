using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Bouncer.Domain.Entities;
using Bouncer.Domain.Interfaces;
using System.Reflection;

namespace Bouncer.Data.Contexts
{
    public class SqlServeDbContext : BaseContext, IDbContext
    {
        public virtual DbSet<Shift> TblShifts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_configuration.GetConnectionString("BouncerDB"));
            base.OnConfiguring(options);

            options.UseLoggerFactory(_loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
                
            base.OnModelCreating(builder);

            builder.Entity<Shift>(entity =>
            {
                entity.Property(e => e.Interval)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

        }
    }
}
