using System.Threading;
using System.Threading.Tasks;
using GoLocal.Domain.Entities;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Artisan.Application.Commands.Shops.UpdateShopContact
{
    public class UpdateShopContactCommandHandler : AbstractRequestHandler<UpdateShopContactCommand>
    {
        private readonly Context _context;

        public UpdateShopContactCommandHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result> Handle(UpdateShopContactCommand request, CancellationToken cancellationToken)
        {
            Shop shop = await _context.Shops.SingleOrDefaultAsync(m => m.Id == request.ShopId, cancellationToken);
            if (shop == null)
                return NotFound<Shop>(request.ShopId);

            request.Adapt(shop);
            _context.Shops.Update(shop);
            await _context.SaveChangesAsync(cancellationToken);

            // TODO: Send an email to the user
            
            return Ok();
        }
    }
}