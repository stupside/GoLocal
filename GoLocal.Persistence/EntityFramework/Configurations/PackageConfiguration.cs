using GoLocal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoLocal.Persistence.EntityFramework.Configurations
{
    public class PackageConfiguration : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> builder)
        {
            builder.HasKey(m => m.Id);

            builder.HasIndex(m => new {m.Name, m.ItemId});

            builder.Property(m => m.Name).IsRequired();
            builder.Property(m => m.Description).IsRequired();
            builder.Property(m => m.Stocks).IsRequired();
            builder.Property(m => m.Price).IsRequired();
        }
    }
}