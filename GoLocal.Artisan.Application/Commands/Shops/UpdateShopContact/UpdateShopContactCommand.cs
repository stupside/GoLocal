using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Artisan.Application.Commands.Shops.UpdateShopContact
{
    public class UpdateShopContactCommand : AbstractRequest
    {
        public int ShopId { get; init; }
        public string Phone { get; init; }
        public string Email { get; init; }
    }
}