using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Artisan.Application.Commands.Commands.GenerateCommandInvoice
{
    [AuthorizedEntity(typeof(Shop))]
    public class GenerateCommandInvoiceCommand : AbstractRequest<int>
    {
        [AuthorizedEntityId]
        public int ShopId { get; init; }
        public string CommandId { get; init; }
    }
}