using System.Threading;
using System.Threading.Tasks;
using GoLocal.Domain.Entities;
using GoLocal.Domain.Entities.Identity;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Accessor.Accessors;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Artisan.Application.Commands.Carts.AddCartPackage
{
    public class AddCartPackageCommandHandler : AbstractRequestHandler<AddCartPackageCommand>
    {        
        private readonly Context _context;
        private readonly IUserAccessor<User> _user;

        public AddCartPackageCommandHandler(Context context, IUserAccessor<User> user)
        {
            _context = context;
            _user = user;
        }

        public override async Task<Result> Handle(AddCartPackageCommand request, CancellationToken cancellationToken)
        {
            User user = await _user.GetUserAsync();

            //TODO : Check if item is product
            Package package = await _context.Packages.Include(m => m.Item).ThenInclude(m => m.Shop)
                .SingleOrDefaultAsync(m => m.Id == request.PackageId &&
                m.ItemId == request.ItemId &&
                m.Item.ShopId == request.ShopId, cancellationToken);
            
            if (package == null)
                return NotFound<Package>(request.PackageId);

            if (package.Stocks < request.Quantity)
                return BadRequest($"Invalid quantity. Only {package.Stocks} were available");

            Cart cart = await _context.Carts.SingleOrDefaultAsync(
                m => m.ShopId == request.ShopId && m.UserId == user.Id, cancellationToken);

            if (cart == null)
            {
                cart = new Cart(user, package.Item.Shop);
                await _context.Carts.AddAsync(cart, cancellationToken);
            }
            
            CartPackage cartPackage = await _context.CartPackages.SingleOrDefaultAsync(
                m => m.CartId == cart.Id && m.PackageId == request.PackageId, cancellationToken);
            
            if (cartPackage == null)
            {
                cartPackage = new CartPackage(cart, package, package.Price, request.Quantity);
                await _context.CartPackages.AddAsync(cartPackage, cancellationToken);
            }
            else
            {
                if (package.Stocks < cartPackage.Quantity + request.Quantity)
                    return BadRequest($"Invalid quantity. Only {package.Stocks - cartPackage.Quantity} were available");
                
                cartPackage.Quantity += request.Quantity;
                _context.CartPackages.Update(cartPackage);
            }
            
            await _context.SaveChangesAsync(cancellationToken);
            return Ok();
        }
    }
}