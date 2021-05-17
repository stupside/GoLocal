using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Artisan.Application.Queries.Items.GetItem
{
    public class GetItemQuery : AbstractRequest<GetItemResponse>
    {
        public int ShopId { get; init; }
        public int ItemId { get; init; }

        public GetItemQuery(int shopId, int itemId)
        {
            ShopId = shopId;
            ItemId = itemId;
        }
    }
}