using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Artisan.Application.Commands.Invoices.MakeInvoiceReady
{
    [AuthorizedEntity(typeof(Shop))]
    public class MakeInvoiceReadyCommand : AbstractRequest
    {
        [AuthorizedEntityId]
        public int ShopId { get; init; }
        public int InvoiceId { get; init; }

        public MakeInvoiceReadyCommand(int shopId, int invoiceId)
        {
            ShopId = shopId;
            InvoiceId = invoiceId;
        }
    }
}