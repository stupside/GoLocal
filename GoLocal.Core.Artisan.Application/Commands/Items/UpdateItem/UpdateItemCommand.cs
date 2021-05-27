using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Artisan.Application.Commands.Items.UpdateItem
{
    [AuthorizedEntity(typeof(Shop))]
    public class UpdateItemCommand : AbstractRequest
    {
        [AuthorizedEntityId]
        public int ShopId { get; init; }
        public int ItemId { get; init; }
        public string OldName { get; init; }
        public string NewName { get; init; }
        public string Description { get; init; }
    }
}