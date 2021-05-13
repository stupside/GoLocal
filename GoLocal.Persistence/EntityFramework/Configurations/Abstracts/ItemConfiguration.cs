using GoLocal.Domain.Entities.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoLocal.Persistence.EntityFramework.Configurations.Abstracts
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(m => m.Id);
            
            builder.HasIndex(m => new {m.Name, m.ShopId}).IsUnique();

            builder.Property(m => m.Name).IsRequired();
            builder.Property(m => m.Description).IsRequired();

            builder.HasMany(m => m.Comments)
                .WithOne()
                .HasForeignKey(m => m.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(m => m.Packages)
                .WithOne()
                .HasForeignKey(m => m.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}