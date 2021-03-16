using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Smeat.Leader.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Core.Entities.CustomerAggregate.Customer> Customers { get; set; }
        DbSet<Core.Entities.CustomerAggregate.Vehicle> Vehicles { get; set; }
    }
}
