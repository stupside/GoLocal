using GoLocal.Domain.Entities.Identity;

namespace GoLocal.Domain.Entities
{
    public class Employee
    {
        public string Id { get; set; }
        
        public Employee(){}

        public Employee(User user, Shop shop)
            : this()
        {
            UserId = user.Id;
            ShopId = shop.Id;
        }
        
        public string UserId { get; }
        public virtual User User { get; }
        
        public int ShopId { get; }
        public virtual Shop Shop { get; }
    }
}