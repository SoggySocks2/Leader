using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Smeat.Leader.Web.Areas.Identity.Entities;

namespace Smeat.Leader.Web.Data
{
    public class AuthDbContext : IdentityDbContext<LeaderUser, LeaderRole, long>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base (options)
        {

        }
    }
}
