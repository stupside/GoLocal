using System;
using System.Collections.Generic;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Domain.Enums;

namespace GoLocal.Core.Domain.Entities
{
    public class Command
    {
        public string Id { get; set; }

        public CommandStatus Status { get; set; }

        public DateTime Creation { get; }
        
        public Command()
        {
            Creation = DateTime.UtcNow;
        }

        public Command(User user, Package package, int shopId, float price, string specification)
            : this()
        {
            UserId = user.Id;
            PackageId = package.Id;
            ShopId = shopId;

            CommandProposals = new List<CommandProposal>
            {
                new (this, user, price, specification)
            };

            Status = CommandStatus.Pending;
        }

        public string UserId { get; }
        public virtual User User { get; }
        
        public int ShopId { get; }
        public virtual Shop Shop { get; }
        
        public int PackageId { get; }
        public virtual Package Package { get; }
        
        public int? InvoiceId { get; set; }
        public virtual Invoice Invoice { get; }
        
        public virtual ICollection<Message> Messages { get; }
        
        public virtual ICollection<CommandProposal> CommandProposals { get; }
    }
}