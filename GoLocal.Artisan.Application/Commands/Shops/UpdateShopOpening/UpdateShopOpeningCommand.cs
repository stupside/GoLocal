using GoLocal.Domain.Enums;
using GoLocal.Domain.ValueObjects;
using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Artisan.Application.Commands.Shops.UpdateShopOpening
{
    public class UpdateShopOpeningCommand : AbstractRequest
    {
        public int ShopId { get; init; }
        public Day Day { get; init; }
        public TimeRange Morning { get; init; }
        public TimeRange Evening { get; init; }
    }
}