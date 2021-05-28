using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Artisan.Application.Commands.Shops.UpdateShopVisibility
{
    [AuthorizedEntity(typeof(Shop))]
    public class UpdateShopVisibilityCommand : AbstractRequest
    {
        [AuthorizedEntityId]
        public int ShopId { get; init; }
        public bool Public { get; init; }
    }
}