using GoLocal.Domain.Enums;
using GoLocal.Domain.ValueObjects;

namespace GoLocal.Client.Application.Queries.Shops.GetShop.Models
{
    public class OpeningDto
    {
        public Day Day { get; init; }
        public TimeRange Morning { get; init; }
        public TimeRange Evening { get; init; }
    }
}