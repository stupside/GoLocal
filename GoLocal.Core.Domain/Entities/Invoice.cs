using System;
using System.Collections.Generic;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Domain.Enums;

namespace GoLocal.Core.Domain.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public string Code { get; init; }
        public InvoiceStatus Status { get; set; }
        public DateTime Creation { get; }

        public Invoice()
        {
            Creation = DateTime.UtcNow;
            Code = Guid.NewGuid().ToString();
        }

        public Invoice(Cart cart)
            : this()
        {
            UserId = cart.UserId;
            ShopId = cart.ShopId;

            InvoiceItems = new List<InvoiceItem>();
            
            foreach (CartPackage package in cart.CartPackages)
                InvoiceItems.Add(new InvoiceItem(this, package.PackageId, package.Quantity, package.Price*package.Quantity, package.Package.Name));
        }

        public Invoice(Command command, CommandProposal proposal)
            : this()
        {
            UserId = command.UserId;
            ShopId = command.ShopId;

            InvoiceItems = new List<InvoiceItem>
            {
                new(this, command.PackageId, 1, proposal.Price, proposal.Specification)
            };
        }

        public string UserId { get; }
        public virtual User User { get; }
        
        public int ShopId { get; }
        public virtual Shop Shop { get; }
        
        public virtual ICollection<InvoiceItem> InvoiceItems { get; }
    }
}