using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Smeat.Leader.Infrastructure.Identity
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        DbSet<Core.Entities.CustomerAggregate.Customer> Customers { get; set; }
        DbSet<Core.Entities.CustomerAggregate.Vehicle> Vehicles { get; set; }
    }
}
