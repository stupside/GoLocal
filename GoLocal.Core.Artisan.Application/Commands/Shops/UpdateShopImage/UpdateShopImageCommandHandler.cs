using System.Threading;
using GoLocal.Bus.Results;
using System.Threading.Tasks;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Artisan.Application.Commons.Helpers;
using GoLocal.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using GoLocal.Core.Persistence.EntityFramework;

namespace GoLocal.Core.Artisan.Application.Commands.Shops.UpdateShopImage
{
    public class UpdateShopImageCommandHandler : AbstractRequestHandler<UpdateShopImageCommand>
    {
        private readonly Context _context;

        public UpdateShopImageCommandHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result> Handle(UpdateShopImageCommand request, CancellationToken cancellationToken)
        {
            Shop shop = await _context.Shops.SingleOrDefaultAsync(m => m.Id == request.ShopId, cancellationToken);
            if (shop == null)
                return NotFound<Shop>(request.ShopId);

            shop.Image = await request.File.ResizeAsync();

            _context.Shops.Update(shop);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}