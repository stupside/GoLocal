using GoLocal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoLocal.Persistence.EntityFramework.Configurations
{
    public class CommandProposalConfiguration: IEntityTypeConfiguration<CommandProposal>
    {
        public void Configure(EntityTypeBuilder<CommandProposal> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Specification).IsRequired();
            builder.Property(m => m.Price).IsRequired();
            
            builder.HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}