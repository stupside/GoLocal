using System;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Domain.Entities.Abstracts;
using GoLocal.Core.Persistence.EntityFramework;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Client.Application.Queries.Items.GetItem
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
                .Include(m => m.Comments)
                .SingleOrDefaultAsync(m => m.Id == request.ItemId && m.ShopId == request.ShopId, cancellationToken);

            if (item == null)
                return NotFound<Item>(request.ItemId);

            _ = TypeAdapterConfig<Item, GetItemResponse>.NewConfig()
                .Map(dest => dest.Image, src => src.Image == null ? null : Convert.ToBase64String(src.Image));

            return Ok(item.Adapt<GetItemResponse>());
        }
    }
}