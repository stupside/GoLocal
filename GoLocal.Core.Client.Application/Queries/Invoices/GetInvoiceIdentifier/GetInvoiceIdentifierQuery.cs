using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Client.Application.Queries.Invoices.GetInvoiceIdentifier
{
    [AuthorizedEntity(typeof(Invoice))]
    public class GetInvoiceIdentifierQuery : AbstractRequest<GetInvoiceIdentifierResponse>
    {
        [AuthorizedEntityId]
        public int InvoiceId { get; init; }

        public GetInvoiceIdentifierQuery(int invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }
}