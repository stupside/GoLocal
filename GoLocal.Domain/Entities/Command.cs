using System;
using System.Collections.Generic;
using GoLocal.Domain.Entities.Identity;
using GoLocal.Domain.Enums;

namespace GoLocal.Domain.Entities
{
    public class Command
    {
        public string Id { get; set; }
        
        public string Specification { get; set; }
        public float Price { get; set; }
        
        public CommandStatus Status { get; set; }

        public DateTime Creation { get; }
        
        public Command()
        {
            Creation = DateTime.UtcNow;
        }

        public Command(User user, Package package, float price, string specification)
            : this()
        {
            UserId = user.Id;
            PackageId = package.Id;

            CommandProposals = new List<CommandProposal>();
            CommandProposals.Add(new CommandProposal(this, user, price, specification));
            
            Status = CommandStatus.PendingPrice;
        }

        public string UserId { get; }
        public virtual User User { get; }
        
        public int PackageId { get; }
        public virtual Package Package { get; }
        
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; }
        
        public virtual ICollection<Message> Messages { get; }
        
        public virtual ICollection<CommandProposal> CommandProposals { get; }
    }
}