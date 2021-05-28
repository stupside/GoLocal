using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Artisan.Application.Commands.Shops.DeleteShop
{
    [AuthorizedEntity(typeof(Shop))]
    public class DeleteShopCommand : AbstractRequest
    {
        [AuthorizedEntityId]
        public int ShopId { get; init; }
        public string Name { get; init; }

        public DeleteShopCommand(int shopId, string name)
        {
            ShopId = shopId;
            Name = name;
        }
    }
}