using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Artisan.Application.Commands.Commands.GenerateCommandInvoice
{
    [AuthorizedEntity(typeof(Command))]
    public class GenerateCommandInvoiceCommand : AbstractRequest<int>
    {
        [AuthorizedEntityId]
        public string CommandId { get; init; }
    }
}