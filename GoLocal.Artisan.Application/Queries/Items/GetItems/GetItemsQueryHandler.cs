using System.Threading;
using System.Threading.Tasks;
using GoLocal.Artisan.Application.Queries.Items.GetItems.Models;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using GoLocal.Shared.Bus.Results.Pages;

namespace GoLocal.Artisan.Application.Queries.Items.GetItems
{
    public class GetItemsQueryHandler : AbstractPagedRequestHandler<GetItemsQuery, ItemDto>
    {
        private readonly Context _context;

        public GetItemsQueryHandler(Context context)
        {
            _context = context;
        }

        public override Task<Result<Page<ItemDto>>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}