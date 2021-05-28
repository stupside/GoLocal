using GoLocal.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoLocal.Core.Persistence.EntityFramework.Configurations
{
    public class CommandConfiguration : IEntityTypeConfiguration<Command>
    {
        public void Configure(EntityTypeBuilder<Command> builder)
        {
            builder.HasKey(m => m.Id);

            builder.HasIndex(m => new {m.UserId, m.PackageId});

            builder.Property(m => m.Price);
            builder.Property(m => m.Specification);
            builder.Property(m => m.Status).IsRequired();

            builder.HasOne(m => m.Package)
                .WithMany()
                .HasForeignKey(m => m.PackageId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(m => m.Messages)
                .WithOne()
                .HasForeignKey(m => m.CommandId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.Invoice)
                .WithOne()
                .HasForeignKey<Command>(m => m.InvoiceId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(m => m.CommandProposals)
                .WithOne(m => m.Command)
                .HasForeignKey(m => m.CommandId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}