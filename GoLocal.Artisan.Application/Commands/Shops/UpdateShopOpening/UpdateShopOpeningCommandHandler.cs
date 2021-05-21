using System;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Domain.Entities;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Artisan.Application.Commands.Shops.UpdateShopOpening
{
    public class UpdateShopOpeningCommandHandler : AbstractRequestHandler<UpdateShopOpeningCommand>
    {
        private readonly Context _context;

        public UpdateShopOpeningCommandHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result> Handle(UpdateShopOpeningCommand request, CancellationToken cancellationToken)
        {
            Shop shop = await _context.Shops.SingleOrDefaultAsync(m => m.Id == request.ShopId, cancellationToken);
            if (shop == null)
                return NotFound<Shop>(request.ShopId);

            Opening opening =
                await _context.Openings.SingleOrDefaultAsync(m => m.Day == request.Day && m.ShopId == request.ShopId,
                    cancellationToken);
            
            if (request.Evening.GetDifference() <= TimeSpan.Zero && request.Evening == request.Morning) 
                // At this point, Zero for morning and zero for evening
                // Means not opened
            {
                if (opening != null)
                {
                    // Day was stored, so we delete
                    _context.Openings.Remove(opening);
                }
                else
                {
                    // Day doesn't exist, so nothing to do.
                    return BadRequest($"No configuration found for {request.Day}");
                }
            }
            else
            {
                if (opening == null)
                    // The configuration doesn't exist
                {
                    opening = new Opening(shop, request.Day, request.Morning, request.Evening);
                    await _context.Openings.AddAsync(opening, cancellationToken);
                }
                else
                {
                    if (request.Evening.GetDifference() <= TimeSpan.Zero && request.Evening == request.Morning)
                        return Ok();
                    
                    opening.Evening = request.Evening;
                    opening.Morning = request.Morning;
                    
                    _context.Openings.Update(opening);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Ok();
        }
        
    }
}