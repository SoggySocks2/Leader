using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smeat.Leader.Core.Entities;

namespace Smeat.Leader.Infrastructure.Data.Config
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            //builder.Property(t => t.Line1)
            //    .HasMaxLength(70)
            //    .IsRequired();

            //builder.Property(t => t.Line2)
            //    .HasMaxLength(70);

            //builder.Property(t => t.Town)
            //    .HasMaxLength(50);

            //builder.Property(t => t.County)
            //    .HasMaxLength(50);

            //builder.Property(t => t.Region)
            //    .HasMaxLength(60);

            //builder.Property(t => t.PostCode)
            //    .HasMaxLength(10);
        }
    }
}
