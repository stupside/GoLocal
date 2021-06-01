using GoLocal.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoLocal.Core.Persistence.EntityFramework.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Code).ValueGeneratedOnAdd();

            builder.HasMany(m => m.InvoiceItems)
                .WithOne(m => m.Invoice)
                .HasForeignKey(m => m.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}