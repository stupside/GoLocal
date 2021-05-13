using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Artisan.Application.Commands.Shops.UpdateShopLocalisation
{
    public class UpdateShopLocalisationCommand : AbstractRequest
    {
        public int ShopId { get; init; }
        public string Street { get; init; }
        public string Zip { get; init; }
        public string Country { get; init; }
        public string City { get; init; }
        public string Address { get; init; }
        public string Region { get; init; }
    }
}