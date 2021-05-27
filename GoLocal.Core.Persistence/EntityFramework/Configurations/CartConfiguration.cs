using GoLocal.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoLocal.Core.Persistence.EntityFramework.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(m => m.Id);

            builder.HasIndex(m => new {m.UserId, m.ShopId}).IsUnique();

            builder.HasOne(m => m.Shop)
                .WithMany()
                .HasForeignKey(m => m.ShopId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(m => m.CartPackages)
                .WithOne()
                .HasForeignKey(m => m.CartId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}