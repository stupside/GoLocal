using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Artisan.Application.Commands.Shops.UpdateShopLocation
{
    [AuthorizedEntity(typeof(Shop))]
    public class UpdateShopLocationCommand : AbstractRequest
    {
        [AuthorizedEntityId]
        public int ShopId { get; init; }
        public string PostCode { get; init; }
        public string Country { get; init; }
        public string Region { get; init; }
        public string City { get; init; }
        public string Street { get; init; }
        public string Address { get; init; }
    }
}