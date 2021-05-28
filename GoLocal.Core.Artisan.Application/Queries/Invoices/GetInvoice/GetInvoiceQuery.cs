using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Artisan.Application.Queries.Invoices.GetInvoice
{
    [AuthorizedEntity(typeof(Invoice))]
    public class GetInvoiceQuery : AbstractRequest<GetInvoiceResponse>
    {
        [AuthorizedEntityId]
        public int InvoiceId { get; init; }

        public GetInvoiceQuery(int invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }
}