using GoLocal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoLocal.Persistence.EntityFramework.Configurations
{
    public class ShopConfiguration : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            builder.HasKey(m => m.Id);

            builder.HasIndex(m => m.Name).IsUnique();

            builder.OwnsOne(m => m.Localisation, m =>
            {
                m.Property(r => r.Street).IsRequired();
                m.Property(r => r.Zip).IsRequired();
                m.Property(r => r.Country).IsRequired();
                m.Property(r => r.City).IsRequired();
                m.Property(r => r.Address).IsRequired();
                m.Property(r => r.Region).IsRequired();
            });
            
            builder.OwnsOne(m => m.Contact, m =>
            {
                m.Property(r => r.Email).IsRequired();
                m.Property(r => r.Phone).IsRequired();
            });

            builder.HasMany(m => m.Employees)
                .WithOne(m => m.Shop)
                .HasForeignKey(m => m.ShopId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(m => m.Openings)
                .WithOne()
                .HasForeignKey(m => m.ShopId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(m => m.ShopCategories)
                .WithOne(m => m.Shop)
                .HasForeignKey(m => m.ShopId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(m => m.Products)
                .WithOne(m => m.Shop)
                .HasForeignKey(m => m.ShopId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(m => m.Services)
                .WithOne(m => m.Shop)
                .HasForeignKey(m => m.Shop)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}