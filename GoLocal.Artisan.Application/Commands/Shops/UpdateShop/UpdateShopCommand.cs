using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Artisan.Application.Commands.Shops.UpdateShop
{
    public class UpdateShopCommand : AbstractRequest
    {
        public int ShopId { get; init; }
        public string OldName { get; init; }
        public string NewName { get; init; }
    }
}