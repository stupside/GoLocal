using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Artisan.Application.Queries.Invoices.GetInvoice
{
    [AuthorizedEntity(typeof(Shop))]
    public class GetInvoiceQuery : AbstractRequest<GetInvoiceResponse>
    {
        [AuthorizedEntityId]
        public int ShopId { get; init; }
        public int InvoiceId { get; init; }

        public GetInvoiceQuery(int shopId, int invoiceId)
        {
            ShopId = shopId;
            InvoiceId = invoiceId;
        }
    }
}