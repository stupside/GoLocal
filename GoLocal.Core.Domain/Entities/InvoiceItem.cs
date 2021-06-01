using System;

namespace GoLocal.Core.Domain.Entities
{
    public class InvoiceItem
    {
        public string Id { get; set; }
        
        public int Quantity { get; }
        public float Price { get; }
        
        public string Description { get; }
        
        public DateTime Creation { get; }

        public InvoiceItem()
        {
            Creation = DateTime.UtcNow;
        }

        public InvoiceItem(Invoice invoice, int packageId, int quantity, float price, string description = null)
            : this()
        {
            InvoiceId = invoice.Id;
            PackageId = packageId;
            
            Quantity = quantity;
            Price = price;
            Description = description;
        }
        
        public InvoiceItem(Invoice invoice, Package package, int quantity, float price, string description = null)
            : this(invoice, package.Id, quantity, price, description)
        {
        }
        
        public int InvoiceId { get; }
        public virtual Invoice Invoice { get; }
        
        public int PackageId { get; }
        public virtual Package Package { get; }
        
        public virtual Comment Comment { get; }
    }
}