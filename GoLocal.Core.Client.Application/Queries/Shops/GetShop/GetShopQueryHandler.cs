using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Persistence.EntityFramework;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Client.Application.Queries.Shops.GetShop
{
    public class GetShopQueryHandler : AbstractRequestHandler<GetShopQuery, GetShopResponse>
    {
        private readonly Context _context;

        public GetShopQueryHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result<GetShopResponse>> Handle(GetShopQuery request, CancellationToken cancellationToken)
        {
            Shop shop = await _context.Shops
                .Include(m => m.Services)
                .Include(m => m.Products)
                .Include(m => m.Openings)
                .Include(m => m.User)
                .SingleOrDefaultAsync(m => m.Id == request.ShopId, cancellationToken);

            if (shop == null)
                return NotFound<Shop>(request.ShopId);

            GetShopResponse response = shop.Adapt<GetShopResponse>();
            
            response.Rate = (await _context.Comments
                .Where(m => m.Item.ShopId == request.ShopId).Select(m => m.Rate)
                .ToListAsync(cancellationToken)).DefaultIfEmpty().Average();
            
            return Ok(response);
        }
    }
}