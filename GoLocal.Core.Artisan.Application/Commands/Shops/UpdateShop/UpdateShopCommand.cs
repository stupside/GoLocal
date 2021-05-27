using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Artisan.Application.Commands.Shops.UpdateShop
{
    [AuthorizedEntity(typeof(Shop))]
    public class UpdateShopCommand : AbstractRequest
    {
        [AuthorizedEntityId]
        public int ShopId { get; init; }
        public string OldName { get; init; }
        public string NewName { get; init; }
    }
}