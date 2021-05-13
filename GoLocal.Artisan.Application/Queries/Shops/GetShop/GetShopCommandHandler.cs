using System.Threading;
using System.Threading.Tasks;
using GoLocal.Domain.Entities;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Artisan.Application.Queries.Shops.GetShop
{
    public class GetShopCommandHandler : AbstractRequestHandler<GetShopCommand, GetShopResponse>
    {
        private readonly Context _context;

        public GetShopCommandHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result<GetShopResponse>> Handle(GetShopCommand request, CancellationToken cancellationToken)
        {
            GetShopResponse shop = await _context.Shops
                .Include(m => m.Services)
                .Include(m => m.Products)
                .Include(m => m.Employees)
                .Include(m => m.Openings)
                .ProjectToType<GetShopResponse>()
                .SingleOrDefaultAsync(m => m.Id == request.ShopId, cancellationToken);
            
            if (shop == null)
                return NotFound<Shop>(request.ShopId);

            return Ok(shop);
        }
    }
}