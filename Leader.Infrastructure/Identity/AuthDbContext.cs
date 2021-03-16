using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Smeat.Leader.SharedKernel.Identity;

namespace Smeat.Leader.Infrastructure.Identity
{
    public class AuthDbContext : IdentityDbContext<LeaderUser, LeaderRole, long>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }
    }
}
