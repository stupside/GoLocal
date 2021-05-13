using GoLocal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoLocal.Persistence.EntityFramework.Configurations
{
    public class OpeningConfiguration : IEntityTypeConfiguration<Opening>
    {
        public void Configure(EntityTypeBuilder<Opening> builder)
        {
            builder.HasKey(m => m.Id);

            builder.HasIndex(m => new {m.Day, m.ShopId}).IsUnique();

            builder.Property(m => m.Day).IsRequired();

            builder.OwnsOne(m => m.Evening, m =>
            {
                m.Property(r => r.Max).IsRequired();
                m.Property(r => r.Min).IsRequired();
            });
            
            builder.OwnsOne(m => m.Morning, m =>
            {
                m.Property(r => r.Max).IsRequired();
                m.Property(r => r.Min).IsRequired();
            });
        }
    }
}