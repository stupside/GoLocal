using System.Threading;
using System.Threading.Tasks;
using GoLocal.Domain.Entities.Abstracts;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Client.Application.Queries.Items.GetItem
{
    public class GetItemQueryHandler : AbstractRequestHandler<GetItemQuery, GetItemResponse>
    {
        private readonly Context _context;

        public GetItemQueryHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result<GetItemResponse>> Handle(GetItemQuery request, CancellationToken cancellationToken)
        {
            Item item = await _context.Items
                .Include(m => m.Packages)
                .SingleOrDefaultAsync(m => m.Id == request.ItemId && m.ShopId == request.ShopId, cancellationToken);

            if (item == null)
                return NotFound<Item>(request.ItemId);

            return Ok(item.Adapt<GetItemResponse>());
        }
    }
}