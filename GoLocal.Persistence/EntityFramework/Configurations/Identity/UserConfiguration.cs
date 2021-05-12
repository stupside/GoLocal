using GoLocal.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoLocal.Persistence.EntityFramework.Configurations.Identity
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(m => m.Id);

            builder.HasIndex(m => new {m.UserName, m.Email}).IsUnique();

            builder.Property(m => m.Email).IsRequired();

            builder.OwnsOne(m => m.Avatar);
        }
    }
}