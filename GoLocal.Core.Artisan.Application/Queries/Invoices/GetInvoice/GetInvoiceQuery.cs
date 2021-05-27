using GoLocal.Bus.Commons.Mediator;

namespace GoLocal.Core.Artisan.Application.Queries.Invoices.GetInvoice
{
    public class GetInvoiceQuery : AbstractRequest<GetInvoiceResponse>
    {
        public int InvoiceId { get; init; }

        public GetInvoiceQuery(int invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }
}