using GoLocal.Core.Domain.Enums;
using GoLocal.Core.Domain.ValueObjects;

namespace GoLocal.Core.Client.Application.Queries.Shops.GetShop.Models
{
    public class OpeningDto
    {
        public Day Day { get; init; }
        public TimeRange Morning { get; init; }
        public TimeRange Evening { get; init; }
    }
}