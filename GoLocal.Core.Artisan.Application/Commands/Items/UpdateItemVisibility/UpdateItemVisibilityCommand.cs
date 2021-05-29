using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Enums;

namespace GoLocal.Core.Artisan.Application.Commands.Items.UpdateItemVisibility
{
    [AuthorizedEntity(typeof(Shop))]
    public class UpdateItemVisibilityCommand : AbstractRequest
    {
        [AuthorizedEntityId]
        public int ShopId { get; init; }
        public int ItemId { get; init; }
        public Visibility Visibility { get; init; }
    }
}