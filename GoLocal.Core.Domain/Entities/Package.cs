using System.Collections.Generic;
using GoLocal.Core.Domain.Entities.Abstracts;
using GoLocal.Core.Domain.Enums;

namespace GoLocal.Core.Domain.Entities
{
    public class Package
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        
        public int Stocks { get; set; }
        public float Price { get; set; }

        public Visibility Visibility { get; set; }

        public Package()
        {
            Visibility = Visibility.Public;
        }

        public Package(Item item, string name, string description, float price, int stocks)
            : this()
        {
            ItemId = item.Id;
            
            Name = name;
            Description = description;
            Price = price;
            Stocks = stocks;
        }

        public int ItemId { get; }
        public virtual Item Item { get; }
        
        public virtual ICollection<InvoiceItem> InvoiceItems { get; }
    }
}