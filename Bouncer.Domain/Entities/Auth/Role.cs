using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bouncer.Domain.Entities.Auth
{
    [Table("tblRoles")]
    public class Role : IdentityRole<long>
    {
        protected const string Admin = "ADMINISTRATOR";
        protected const string Visitor = "VISITOR";
    }

    [Table("tblRoleClaims")]
    public class RoleClaim : IdentityRoleClaim<long>
    {
    }
}