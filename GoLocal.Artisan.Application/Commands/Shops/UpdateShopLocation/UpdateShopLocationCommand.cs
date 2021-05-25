using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Artisan.Application.Commands.Shops.UpdateShopLocation
{
    public class UpdateShopLocationCommand : AbstractRequest
    {
        public int ShopId { get; init; }
        public string PostCode { get; init; }
        public string Country { get; init; }
        public string Region { get; init; }
        public string City { get; init; }
        public string Street { get; init; }
        public string Address { get; init; }
    }
}