using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Client.Application.Queries.Shops.GetShop
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