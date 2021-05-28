using GoLocal.Bus.Commons.Mediator;

namespace GoLocal.Core.Client.Application.Commands.Carts.GenerateCartInvoice
{
    public class GenerateCartInvoiceCommand : AbstractRequest<int>
    {
        public int ShopId { get; init; }
    }
}