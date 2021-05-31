using GoLocal.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoLocal.Core.Persistence.EntityFramework.Configurations
{
    public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedOnAdd();
            
            builder.HasIndex(m => new {m.InvoiceId, m.PackageId}).IsUnique();
            
            builder.Property(m => m.Quantity).IsRequired();
            builder.Property(m => m.Description).IsRequired();
            builder.Property(m => m.Price).IsRequired();

            builder.HasOne(m => m.Package)
                .WithMany(m => m.InvoiceItems)
                .HasForeignKey(m => m.PackageId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.Comment)
                .WithOne(m => m.InvoiceItem)
                .HasForeignKey<Comment>(m => m.InvoiceItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}