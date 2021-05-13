using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Artisan.Application.Queries.Shops.GetShop
{
    public class GetShopCommand : AbstractRequest<GetShopResponse>
    {
        public int ShopId { get; init; }

        public GetShopCommand(int shopId)
        {
            ShopId = shopId;
        }
    }
}