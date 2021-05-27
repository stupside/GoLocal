using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Persistence.EntityFramework;
using GoLocal.Shared.Mailing.Commons.Models;
using GoLocal.Shared.Mailing.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Artisan.Application.Commands.Shops.UpdateShopContact
{
    public class UpdateShopContactCommandHandler : AbstractRequestHandler<UpdateShopContactCommand>
    {
        private readonly Context _context;
        private readonly IEmailService _email;
        private readonly IUserAccessor<User> _user;
        public UpdateShopContactCommandHandler(Context context, IEmailService email, IUserAccessor<User> user)
        {
            _context = context;
            _email = email;
            _user = user;
        }

        public override async Task<Result> Handle(UpdateShopContactCommand request, CancellationToken cancellationToken)
        {
            User user = await _user.GetUserAsync();
            
            Shop shop = await _context.Shops.SingleOrDefaultAsync(m => m.Id == request.ShopId, cancellationToken);
            if (shop == null)
                return NotFound<Shop>(request.ShopId);

            request.Adapt(shop.Contact);
            
            _context.Shops.Update(shop);
            await _context.SaveChangesAsync(cancellationToken);

            await _email.SendAsync(new EmailMessage(new HashSet<string>{user.Email, shop.Contact.Email}, $"Shop {shop.Name} updated",
                $"Dear {user.UserName},\n You successfully updated the contact information of your shop '{shop.Name}'"), cancellationToken);

            return Ok();
        }
    }
}