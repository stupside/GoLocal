using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Artisan.Application.Commands.Items.UpdateItemDescription
{
    [AuthorizedEntity(typeof(Shop))]
    public class UpdateItemDescriptionCommand : AbstractRequest
    {
        [AuthorizedEntityId]
        public int ShopId { get; init; }
        public int ItemId { get; init; }
        public string Description { get; init; }
    }
}