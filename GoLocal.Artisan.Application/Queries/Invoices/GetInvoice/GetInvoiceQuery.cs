using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Artisan.Application.Queries.Invoices.GetInvoice
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