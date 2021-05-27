using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Artisan.Application.Commands.Shops.UpdateShopContact
{
    [AuthorizedEntity(typeof(Shop))]
    public class UpdateShopContactCommand : AbstractRequest
    {
        [AuthorizedEntityId]
        public int ShopId { get; init; }
        public string Phone { get; init; }
        public string Email { get; init; }
    }
}