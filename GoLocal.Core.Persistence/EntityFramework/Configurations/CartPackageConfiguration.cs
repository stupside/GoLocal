using GoLocal.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoLocal.Core.Persistence.EntityFramework.Configurations
{
    public class CartPackageConfiguration : IEntityTypeConfiguration<CartPackage>
    {
        public void Configure(EntityTypeBuilder<CartPackage> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedOnAdd();
            
            builder.HasIndex(m => new {m.CartId, m.PackageId}).IsUnique();

            builder.Property(m => m.Price).IsRequired();
            builder.Property(m => m.Quantity).IsRequired();

            builder.HasOne(m => m.Package)
                .WithMany()
                .HasForeignKey(m => m.PackageId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}