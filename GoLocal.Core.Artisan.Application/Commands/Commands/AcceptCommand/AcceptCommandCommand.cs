using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Artisan.Application.Commands.Commands.AcceptCommand
{
    [AuthorizedEntity(typeof(Command))]
    public class AcceptCommandCommand : AbstractRequest
    {
        [AuthorizedEntityId]
        public string CommandId { get; init; }
        public bool Accept { get; init; }
    }
}