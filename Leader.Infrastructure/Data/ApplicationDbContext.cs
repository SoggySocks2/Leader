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
        public DbSet<Core.Entities.Customer> Customers { get; set; }
        DbSet<Core.Entities.Vehicle> Vehicles { get; set; }
        DbSet<Core.Entities.Address> Addresses { get; set; }
    }
}
