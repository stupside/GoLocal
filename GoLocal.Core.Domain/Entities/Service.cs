using GoLocal.Core.Domain.Entities.Abstracts;

namespace GoLocal.Core.Domain.Entities
{
    public class Service : Item
    {
        public Service()
        {
        }
        
        public Service(Shop shop, string name, string description, bool hidden = false, bool available = true)
            : base(shop, name, description, hidden, available)
        {
        }
    }
}