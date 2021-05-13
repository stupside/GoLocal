using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Artisan.Application.Commands.Shops.DeleteShop
{
    public class DeleteShopCommand : AbstractRequest
    {
        public int ShopId { get; init; }
        public string Name { get; init; }
    }
}