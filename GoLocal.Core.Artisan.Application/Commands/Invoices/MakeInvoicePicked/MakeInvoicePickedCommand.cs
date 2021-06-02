using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Artisan.Application.Commands.Invoices.MakeInvoicePicked
{
    [AuthorizedEntity(typeof(Shop))]
    public class MakeInvoicePickedCommand : AbstractRequest
    {
        [AuthorizedEntityId]
        public int ShopId { get; init; }
        public string Identifier { get; init; }

        public MakeInvoicePickedCommand(int shopId, string identifier)
        {
            ShopId = shopId;
            Identifier = identifier;
        }
    }
}