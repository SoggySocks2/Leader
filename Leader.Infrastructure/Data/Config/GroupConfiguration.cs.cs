using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smeat.Leader.Core.Entities;

namespace Smeat.Leader.Infrastructure.Data.Config
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.Property(t => t.GroupName)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
