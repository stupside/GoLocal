using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Artisan.Application.Queries.Commands.GetCommand
{
    [AuthorizedEntity(typeof(Shop))]
    public class GetCommandQuery : AbstractRequest
    {
        [AuthorizedEntityId]
        public int ShopId { get; init; }
        public string UserId { get; init; }

        public GetCommandQuery(int shopId, string userId)
        {
            ShopId = shopId;
            UserId = userId;
        }
    }
}