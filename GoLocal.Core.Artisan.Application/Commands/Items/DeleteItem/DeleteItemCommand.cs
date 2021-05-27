using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Artisan.Application.Commands.Items.DeleteItem
{
    [AuthorizedEntity(typeof(Shop))]
    public class DeleteItemCommand : AbstractRequest
    {
        [AuthorizedEntityId]
        public int ShopId { get; init; }
        public int ItemId { get; init; }
        public string Name { get; init; }
    }
}