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
        public string CommandId { get; init; }

        public GetCommandQuery(int shopId, string commandId)
        {
            ShopId = shopId;
            CommandId = commandId;
        }
    }
}