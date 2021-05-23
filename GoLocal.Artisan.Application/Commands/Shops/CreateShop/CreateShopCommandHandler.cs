using System.Threading;
using System.Threading.Tasks;
using GoLocal.Domain.Entities;
using GoLocal.Domain.Entities.Identity;
using GoLocal.Domain.ValueObjects;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Accessor.Accessors;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Artisan.Application.Commands.Shops.CreateShop
{
    public class CreateShopCommandHandler : AbstractRequestHandler<CreateShopCommand, int>
    {
        private readonly IUserAccessor<User> _accessor;
        private readonly Context _context;
        
        public CreateShopCommandHandler(IUserAccessor<User> accessor, Context context)
        {
            _accessor = accessor;
            _context = context;
        }

        public override async Task<Result<int>> Handle(CreateShopCommand request, CancellationToken cancellationToken)
        {
            User user = await _accessor.GetUserAsync();

            if (await _context.Shops.AnyAsync(m => m.Name == request.Name, cancellationToken))
                return BadRequest($"A shop named {request.Name} already exists");

            Shop shop = new Shop(user, request.Name, request.Contact.Adapt<Contact>(), request.Localisation.Adapt<Location>());

            // TODO: Get X, Y from localisation
            
            await _context.Shops.AddAsync(shop, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Ok(shop.Id);
        }
    }
}