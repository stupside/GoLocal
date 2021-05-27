using System;
using GoLocal.Core.Domain.Entities.Abstracts;

namespace GoLocal.Core.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        
        public int Rate { get; }
        public string Body { get; }
        
        public DateTime Creation { get; }

        public Comment()
        {
            Creation = DateTime.UtcNow;
        }

        public Comment(Item item, InvoiceItem invoiceItem, int rate, string body)
            : this()
        {
            InvoiceItemId = invoiceItem.Id;
            ItemId = item.Id;

            Rate = rate;
            Body = body;
        }

        public int ItemId { get; }
        public virtual Item Item { get; }
        
        public string InvoiceItemId { get; }
        public virtual InvoiceItem InvoiceItem { get; }
    }
}