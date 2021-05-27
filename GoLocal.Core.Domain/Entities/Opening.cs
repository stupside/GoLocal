using GoLocal.Core.Domain.Enums;
using GoLocal.Core.Domain.ValueObjects;

namespace GoLocal.Core.Domain.Entities
{
    public class Opening
    {
        public int Id { get; set; }
        
        public Day Day { get; set; }
        public TimeRange Morning { get; set; }
        public TimeRange Evening { get; set; }
        
        public Opening(){}

        public Opening(Shop shop, Day day, TimeRange morning, TimeRange evening)
            : this()
        {
            ShopId = shop.Id;
            
            Day = day;
            Morning = morning;
            Evening = evening;
        }
        
        public int ShopId { get; }
    }
}