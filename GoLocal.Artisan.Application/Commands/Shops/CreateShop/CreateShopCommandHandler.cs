using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Domain.Entities;
using GoLocal.Domain.Entities.Identity;
using GoLocal.Domain.ValueObjects;
using GoLocal.Shared.Accessor.Accessors;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using GoLocal.Shared.Locate.Interfaces;
using GoLocal.Shared.Locate.Models;
using GoLocal.Shared.Locate.Models.Search;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Context = GoLocal.Persistence.EntityFramework.Context;

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
            Place place = await _locate.GetPosition(location.Address, location.Street, location.City, location.PostCode,
                location.Country);
            
            if (place is not {Any: true})
                return BadRequest("Something went wrong when we tried to localize your shop");

            Feature feature = place.Feature;
            
            Shop shop = new Shop(user, request.Name, request.Contact.Adapt<Contact>(), feature.Adapt<Location>());

            await _context.Shops.AddAsync(shop, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Ok(shop.Id);
        }
    }
}