using System.Threading;
using System.Threading.Tasks;
using GoLocal.Domain.Entities;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Artisan.Application.Commands.Shops.DeleteShop
{
    public class DeleteShopCommandHandler : AbstractRequestHandler<DeleteShopCommand>
    {
        private readonly Context _context;

        public DeleteShopCommandHandler(Context context)
        {
            _context = context;
        }
        
        public override async Task<Result<Unit>> Handle(DeleteShopCommand request, CancellationToken cancellationToken)
        {
            Shop shop = await _context.Shops.SingleOrDefaultAsync(m => m.Id == request.ShopId, cancellationToken);
            if (shop == null)
                return NotFound<Shop>(request.ShopId);
            
            if(shop.Name != request.Name)
                return BadRequest("Name doesn't match");

            _context.Shops.Remove(shop);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}