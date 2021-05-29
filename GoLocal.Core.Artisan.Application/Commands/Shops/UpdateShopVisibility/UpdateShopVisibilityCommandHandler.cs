using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Enums;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Artisan.Application.Commands.Shops.UpdateShopVisibility
{
    public class UpdateShopVisibilityCommandHandler : AbstractRequestHandler<UpdateShopVisibilityCommand>
    {
        private readonly Context _context;

        public UpdateShopVisibilityCommandHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result> Handle(UpdateShopVisibilityCommand request, CancellationToken cancellationToken)
        {
            Shop shop = await _context.Shops.SingleOrDefaultAsync(m => m.Id == request.ShopId, cancellationToken);
            if (shop == null)
                return NotFound<Shop>();

            if (shop.Visibility == request.Visibility)
                return Ok();

            shop.Visibility = request.Visibility;
            
            _context.Shops.Update(shop);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}