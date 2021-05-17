using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Client.Application.Queries.GetShop
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