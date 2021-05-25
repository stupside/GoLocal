using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Domain.Entities;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Client.Application.Queries.Shops.GetShop
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
            response.Rate = await _context.Comments
                .Where(m => m.Item.ShopId == request.ShopId)
                .AverageAsync(m => m.Rate, cancellationToken);;
            
            return Ok(response);
        }
    }
}