using System;
using System.Collections.Generic;
using GoLocal.Domain.Entities.Identity;

namespace GoLocal.Domain.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        
        public DateTime Creation { get; }

        public Invoice()
        {
            Creation = DateTime.UtcNow;
        }

        public Invoice(Cart cart)
            : this()
        {
            UserId = cart.UserId;

            InvoiceItems = new List<InvoiceItem>();
            
            foreach (CartPackage package in cart.CartPackages)
                InvoiceItems.Add(new InvoiceItem(this, package.PackageId, package.Quantity, package.Price*package.Quantity));
        }

        public Invoice(Command command)
            : this()
        {
            UserId = command.UserId;

            InvoiceItems = new List<InvoiceItem>
            {
                new InvoiceItem(this, command.PackageId, 1, command.Price, command.Specification)
            };

        }

        public string UserId { get; }
        public virtual User User { get; }
        
        public virtual ICollection<InvoiceItem> InvoiceItems { get; }
    }
}