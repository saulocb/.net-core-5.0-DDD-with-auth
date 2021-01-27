using Bouncer.Commoun.Enums;
using Bouncer.Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OxiFin.Data.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
            new Role
            {
                Id = (long)ERole.ADMINISTRATOR,
                Name = ERole.ADMINISTRATOR.ToString().ToLower(),
                NormalizedName = ERole.ADMINISTRATOR.ToString()
            },
            new Role
            {
                Id = (long)ERole.VISITOR,
                Name = ERole.VISITOR.ToString().ToLower(),
                NormalizedName = ERole.VISITOR.ToString()
            });
        }
    }
}
