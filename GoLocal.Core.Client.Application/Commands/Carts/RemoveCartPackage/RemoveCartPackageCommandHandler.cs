using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Client.Application.Commands.Carts.RemoveCartPackage
{
    public class RemoveCartPackageCommandHandler : AbstractRequestHandler<RemoveCartPackageCommand>
    {
        private readonly Context _context;
        private readonly IUserAccessor<User> _user;

        public RemoveCartPackageCommandHandler(Context context, IUserAccessor<User> user)
        {
            _context = context;
            _user = user;
        }

        public override async Task<Result> Handle(RemoveCartPackageCommand request, CancellationToken cancellationToken)
        {
            User user = await _user.GetUserAsync();

            // 1. Get the cart
            Cart cart = await _context.Carts.SingleOrDefaultAsync(
                m => m.ShopId == request.ShopId && m.UserId == user.Id, cancellationToken);

            // 2. If the cart doesn't exist, it means there is no cart packages
            if (cart == null)
                return NotFound<Cart>();

            // 3. Cart exist so we can query for the cart package
            CartPackage cartPackage = await _context.CartPackages.SingleOrDefaultAsync(
                m => m.PackageId == request.PackageId && m.CartId == cart.Id, cancellationToken);

            // 4. If the cart package doesn't exist, it means we cannot decrease the quantity
            if (cartPackage == null)
                return NotFound<CartPackage>();

            // 5. We check if we are removing everything
            if (cartPackage.Quantity <= request.Quantity)
            {
                // 5.1 If 100% of the quantity the remove cart package
                _context.Remove(cartPackage);

                // 5.2 If we don't have any cart packages in the cart anymore, then we can delete the cart
                if (await _context.CartPackages.AnyAsync(m => m.CartId == cart.Id, cancellationToken))
                    _context.Remove(cart);
            }
            else if(cartPackage.Quantity > request.Quantity)
            {
                // 5.1 If not 100% of the quantity, then  we are safe and we can decrease the quantity in cart package
                cartPackage.Quantity -= request.Quantity;
                _context.Update(cartPackage.Quantity);
            }
            
            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}