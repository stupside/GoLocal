using System.Threading;
using System.Threading.Tasks;
using GoLocal.Domain.Entities;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Artisan.Application.Commands.Shops.UpdateShop
{
    public class UpdateShopCommandHandler : AbstractRequestHandler<UpdateShopCommand>
    {
        private readonly Context _context;

        public UpdateShopCommandHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result<Unit>> Handle(UpdateShopCommand request, CancellationToken cancellationToken)
        {
            Shop shop = await _context.Shops.SingleOrDefaultAsync(m => m.Id == request.ShopId, cancellationToken);
            if (shop == null)
                return NotFound<Shop>(request.ShopId);

            if (shop.Name != request.OldName)
                return BadRequest("Old name doesn't match");

            if (request.OldName == request.NewName)
                return Ok();
            
            if(await _context.Shops.AnyAsync(m => m.Id != request.ShopId && m.Name == request.NewName, cancellationToken))
                return BadRequest($"A shop named {request.NewName} already exists");

            shop.Name = request.NewName;

            _context.Shops.Update(shop);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}