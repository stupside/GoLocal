using GoLocal.Domain.Enums;
using GoLocal.Domain.ValueObjects;

namespace GoLocal.Artisan.Application.Queries.Shops.GetShop.Models
{
    public class OpeningDto
    {
        public Day Day { get; set; }
        public TimeRange Morning { get; set; }
        public TimeRange Evening { get; set; }
    }
}