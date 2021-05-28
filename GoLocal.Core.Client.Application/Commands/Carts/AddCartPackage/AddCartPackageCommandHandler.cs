using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Entities.Abstracts;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Client.Application.Commands.Carts.AddCartPackage
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
            // 1. Check if item is a service (itemId, packageId)
            if (await _context.Services.AnyAsync(m => m.Id == request.ItemId, cancellationToken))
                return NotFound<Item>(request.ItemId);
            
            // 2. We get the package and the item
            Package package = await _context.Packages
                .Include(m => m.Item)
                .SingleOrDefaultAsync(m => m.Id == request.PackageId && m.ItemId == request.ItemId && m.Item.ShopId == request.ShopId, cancellationToken);

            if (package == null)
                return NotFound<Package>(request.PackageId);

            // 3. Enough stocks ?
            if (package.Stocks < request.Quantity)
                return BadRequest($"Invalid quantity. Only {package.Stocks} stocks were available");

            User user = await _user.GetUserAsync();
            
            // 4. Get user cart for shop
            Cart cart = await _context.Carts.SingleOrDefaultAsync(m => m.ShopId == request.ShopId && m.UserId == user.Id, cancellationToken);
            if (cart == null)
            {
                // 4.1 Cart doesn't exist, we create one
                cart = new Cart(user, package.Item.ShopId);
                await _context.Carts.AddAsync(cart, cancellationToken);
            }
            
            // 5. We get the cart package from cart
            CartPackage cartPackage = await _context.CartPackages
                .SingleOrDefaultAsync(m => m.CartId == cart.Id && m.PackageId == request.PackageId, cancellationToken);
            
            if (cartPackage == null)
            {
                // 5.1 This package doesn't exist in the cart so we create one
                cartPackage = new CartPackage(cart, package, package.Price, request.Quantity);
                await _context.CartPackages.AddAsync(cartPackage, cancellationToken);
            }
            else
            {
                // 5.2 This package is already in the cart, so we check if the total quantity is still valid
                if (package.Stocks < cartPackage.Quantity + request.Quantity)
                    return BadRequest($"Invalid quantity. Only {package.Stocks - cartPackage.Quantity} stocks were available");
                
                cartPackage.Quantity += request.Quantity;
                _context.CartPackages.Update(cartPackage);
            }
            
            // 6. If we go that far, it means everything is ok
            await _context.SaveChangesAsync(cancellationToken);
            return Ok();
        }
    }
}