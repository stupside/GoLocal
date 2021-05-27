using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Client.Application.Queries.Shops.GetShop
{
    [AuthorizedEntity(typeof(Shop))]
    public class GetShopQuery : AbstractRequest<GetShopResponse>
    {
        [AuthorizedEntityId]
        public int ShopId { get; init; }

        public GetShopQuery(int shopId)
        {
            ShopId = shopId;
        }
    }
}