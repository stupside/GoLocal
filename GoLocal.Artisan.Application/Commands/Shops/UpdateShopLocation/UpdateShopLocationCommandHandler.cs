using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Domain.Entities;
using GoLocal.Domain.Entities.Identity;
using GoLocal.Shared.Accessor.Accessors;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using GoLocal.Shared.Locate.Interfaces;
using GoLocal.Shared.Locate.Models;
using GoLocal.Shared.Locate.Models.Search;
using GoLocal.Shared.Mailing.Commons.Models;
using GoLocal.Shared.Mailing.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Context = GoLocal.Persistence.EntityFramework.Context;

namespace GoLocal.Artisan.Application.Commands.Shops.UpdateShopLocation
{
    public class UpdateShopLocationCommandHandler : AbstractRequestHandler<UpdateShopLocationCommand>
    {
        private readonly Context _context;
        private readonly ILocateService _locate;
        private readonly IEmailService _email;
        private readonly IUserAccessor<User> _user;
        public UpdateShopLocationCommandHandler(Context context, IEmailService email, IUserAccessor<User> user, ILocateService locate)
        {
            _context = context;
            _email = email;
            _user = user;
            _locate = locate;
        }
        
        public override async Task<Result> Handle(UpdateShopLocationCommand request, CancellationToken cancellationToken)
        {
            User user = await _user.GetUserAsync();
            
            Shop shop = await _context.Shops.SingleOrDefaultAsync(m => m.Id == request.ShopId, cancellationToken);
            if (shop == null)
                return NotFound<Shop>(request.ShopId);
            
            Place place = await _locate.GetPosition(request.Address, request.Street, request.City, request.PostCode,
                request.Country);
            
            if (place == null || !place.Features.Any())
                return BadRequest("Something went wrong when we tried to localize your shop");
            
            Feature feature = place.Features.First();
            
            feature.Adapt(shop.Location);
            
            _context.Shops.Update(shop);
            await _context.SaveChangesAsync(cancellationToken);

            await _email.SendAsync(new EmailMessage(new HashSet<string>{user.Email, shop.Contact.Email}, $"Shop {shop.Name} updated",
                $"Dear {user.UserName},\nYou successfully updated the location of your shop {shop.Name}"), cancellationToken);
            
            return Ok();
        }
    }
}