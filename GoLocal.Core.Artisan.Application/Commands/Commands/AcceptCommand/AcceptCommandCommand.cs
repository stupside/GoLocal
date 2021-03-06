using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Artisan.Application.Commands.Commands.AcceptCommand
{
    [AuthorizedEntity(typeof(Shop))]
    public class AcceptCommandCommand : AbstractRequest
    {
        [AuthorizedEntityId]
        public int ShopId { get; init; }
        public string CommandId { get; init; }
        public bool Accept { get; init; }
    }
}