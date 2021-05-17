using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Artisan.Application.Commands.Items.DeleteItem
{
    public class DeleteItemCommand : AbstractRequest
    {
        public int ShopId { get; init; }
        public int ItemId { get; init; }
        public string Name { get; init; }
    }
}