using System.Collections.Generic;
using GoLocal.Domain.Entities.Abstracts;

namespace GoLocal.Domain.Entities
{
    public class Package
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        
        public int Stocks { get; set; }
        public float Price { get; set; }
        
        public bool Hidden { get; set; }
        public bool Available { get; set; }
        
        public Package(){}

        public Package(Item item, string name, string description, float price, int stocks, bool hidden = false, bool available = true)
            : this()
        {
            ItemId = item.Id;
            
            Name = name;
            Description = description;
            Price = price;
            Stocks = stocks;
            Hidden = hidden;
            Available = available;
        }

        public string ItemId { get; }
        public virtual Item Item { get; }
        
        public virtual ICollection<InvoiceItem> InvoiceItems { get; }
    }
}