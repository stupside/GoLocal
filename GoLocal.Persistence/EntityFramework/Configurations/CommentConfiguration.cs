using GoLocal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoLocal.Persistence.EntityFramework.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(m => m.Id);

            builder.HasIndex(m => new {m.InvoiceItemId, m.ItemId}).IsUnique();

            builder.Property(m => m.Rate).IsRequired();
            builder.Property(m => m.Body).IsRequired();
        }
    }
}