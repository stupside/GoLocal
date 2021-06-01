using GoLocal.Bus.Commons.Mediator;

namespace GoLocal.Core.Client.Application.Queries.Shops.GetShop
{
    public class GetShopQuery : AbstractRequest<GetShopResponse>
    {
        public int ShopId { get; init; }

        public GetShopQuery(int shopId)
        {
            ShopId = shopId;
        }
    }
}