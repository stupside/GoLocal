using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Enums;
using GoLocal.Core.Domain.ValueObjects;

namespace GoLocal.Core.Artisan.Application.Commands.Shops.UpdateShopOpening
{
    [AuthorizedEntity(typeof(Shop))]
    public class UpdateShopOpeningCommand : AbstractRequest
    {
        [AuthorizedEntityId]
        public int ShopId { get; init; }
        public Day Day { get; init; }
        public TimeRange Morning { get; init; }
        public TimeRange Evening { get; init; }
    }
}