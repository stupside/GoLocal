using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Domain.Entities;
using GoLocal.Domain.Entities.Identity;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Accessor.Accessors;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using GoLocal.Shared.Mailing.Commons.Models;
using GoLocal.Shared.Mailing.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Artisan.Application.Commands.Shops.UpdateShopLocation
{
    public class UpdateShopLocationCommandHandler : AbstractRequestHandler<UpdateShopLocationCommand>
    {
        private readonly Context _context;
        private readonly IEmailService _email;
        private readonly IUserAccessor<User> _user;
        public UpdateShopLocationCommandHandler(Context context, IEmailService email, IUserAccessor<User> user)
        {
            _context = context;
            _email = email;
            _user = user;
        }
        
        public override async Task<Result> Handle(UpdateShopLocationCommand request, CancellationToken cancellationToken)
        {
            User user = await _user.GetUserAsync();
            
            Shop shop = await _context.Shops.SingleOrDefaultAsync(m => m.Id == request.ShopId, cancellationToken);
            if (shop == null)
                return NotFound<Shop>(request.ShopId);

            request.Adapt(shop.Location);
            
            _context.Shops.Update(shop);
            await _context.SaveChangesAsync(cancellationToken);

            await _email.SendAsync(new EmailMessage(new HashSet<string>{user.Email, shop.Contact.Email}, $"Shop {shop.Name} updated",
                $@"
Dear {user.UserName}, 
    You succefully updated the location of your shop {shop.Name}"), cancellationToken);
            
            return Ok();
        }
    }
}