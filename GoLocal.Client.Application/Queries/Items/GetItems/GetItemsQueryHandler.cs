using System.Threading;
using System.Threading.Tasks;
using GoLocal.Client.Application.Queries.Items.GetItems.Models;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using GoLocal.Shared.Bus.Results.Pages;

namespace GoLocal.Client.Application.Queries.Items.GetItems
{
    public class GetItemsQueryHandler : AbstractPagedRequestHandler<GetItemsQuery, ItemDto>
    {
        public override Task<Result<Page<ItemDto>>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}