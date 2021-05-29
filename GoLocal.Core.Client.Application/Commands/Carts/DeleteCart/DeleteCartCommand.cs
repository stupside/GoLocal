using GoLocal.Bus.Commons.Mediator;

namespace GoLocal.Core.Client.Application.Commands.Carts.DeleteCart
{
    public class DeleteCartCommand : AbstractRequest
    {
        public int ShopId { get; init; }

        public DeleteCartCommand(int shopId)
        {
            ShopId = shopId;
        }
    }
}