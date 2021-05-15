using GoLocal.Domain.Entities.Abstracts;

namespace GoLocal.Domain.Entities
{
    public class Product : Item
    {
        public Product()
        {
        }
        
        public Product(Shop shop, string name, string description, bool hidden = false, bool available = true) 
            : base(shop, name, description, hidden, available)
        {
        }

    }
}