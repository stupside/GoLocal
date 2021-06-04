using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Domain.ValueObjects;
using GoLocal.Shared.Locate.Interfaces;
using GoLocal.Shared.Locate.Models.Search;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Context = GoLocal.Core.Persistence.EntityFramework.Context;

namespace GoLocal.Core.Artisan.Application.Commands.Shops.CreateShop
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
            
            Place place = await _locate.GetPosition(request.Location.Address,
                request.Location.Street, request.Location.PostCode, request.Location.City,
                request.Location.Country);
            
            if (place is not {Any: true})
                return BadRequest("Something went wrong when we tried to localize your shop");
            
            Feature feature = place.Feature;
            if(!feature.IsValid)
                return BadRequest($"We didn't found any relevant location (%{feature.Relevance} accuracy)");

            Contact contact = new Contact(request.Contact.Email, request.Contact.Phone);
            Shop shop = new Shop(user, request.Name, contact, feature.Adapt<Location>());

            await _context.Shops.AddAsync(shop, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Ok(shop.Id);
        }
    }
}