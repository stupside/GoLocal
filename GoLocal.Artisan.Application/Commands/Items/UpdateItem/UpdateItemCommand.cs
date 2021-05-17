using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Artisan.Application.Commands.Items.UpdateItem
{
    public class UpdateItemCommand : AbstractRequest
    {
        public int ShopId { get; init; }
        public int ItemId { get; init; }
        public string OldName { get; init; }
        public string NewName { get; init; }
        public string Description { get; init; }
    }
}