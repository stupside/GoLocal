using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Domain.Entities;
using GoLocal.Domain.Entities.Identity;
using GoLocal.Domain.ValueObjects;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Accessor.Accessors;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using GoLocal.Shared.Locate.Interfaces;
using GoLocal.Shared.Locate.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Artisan.Application.Commands.Shops.CreateShop
{
    public class CreateShopCommandHandler : AbstractRequestHandler<CreateShopCommand, int>
    {
        private readonly ILocateService _locate;
        private readonly IUserAccessor<User> _accessor;
        private readonly Context _context;
        
        public CreateShopCommandHandler(IUserAccessor<User> accessor, Context context, ILocateService locate)
        {
            _accessor = accessor;
            _context = context;
            _locate = locate;
        }

        public override async Task<Result<int>> Handle(CreateShopCommand request, CancellationToken cancellationToken)
        {
            User user = await _accessor.GetUserAsync();

            if (await _context.Shops.AnyAsync(m => m.Name == request.Name, cancellationToken))
                return BadRequest($"A shop named {request.Name} already exists");
            
            var location = request.Location;
            Place place = await _locate.GetPosition(location.Address, location.Street, location.City, location.Zip,
                location.Country);
            
            if (place == null || !place.Features.Any())
                return BadRequest("Something went wrong when we tried to localize your shop");
                
            Feature feature = place.Features.First();
            
            Shop shop = new Shop(user, request.Name, request.Contact.Adapt<Contact>(), request.Location.Adapt<Location>());

            shop.Location.Longitude = feature.Longitude;
            shop.Location.Latitude = feature.Latitude;

            await _context.Shops.AddAsync(shop, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Ok(shop.Id);
        }
    }
}