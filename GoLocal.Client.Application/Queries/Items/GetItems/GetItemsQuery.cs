using GoLocal.Client.Application.Queries.Items.GetItems.Models;
using GoLocal.Domain.Entities.Abstracts;
using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Client.Application.Queries.Items.GetItems
{
    public class GetItemsQuery : AbstractPagedRequest<Item, ItemDto>
    {
        public int ShopId { get; init; }
    }
}