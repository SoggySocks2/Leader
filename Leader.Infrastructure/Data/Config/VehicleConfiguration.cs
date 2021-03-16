using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smeat.Leader.Core.Entities;

namespace Smeat.Leader.Infrastructure.Data.Config
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            //builder.Property(t => t.Make)
            //    .HasMaxLength(30)
            //    .IsRequired();
        }
    }
}
