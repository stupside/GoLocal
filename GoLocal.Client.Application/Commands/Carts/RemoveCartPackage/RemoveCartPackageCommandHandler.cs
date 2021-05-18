using System.Threading;
using System.Threading.Tasks;
using GoLocal.Domain.Entities;
using GoLocal.Domain.Entities.Identity;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Accessor.Accessors;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Client.Application.Commands.Carts.RemoveCartPackage
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

            Cart cart = await _context.Carts.SingleOrDefaultAsync(
                m => m.ShopId == request.ShopId && m.UserId == user.Id, cancellationToken);

            if (cart == null)
                return NotFound<Cart>();

            CartPackage cartPackage = await _context.CartPackages.SingleOrDefaultAsync(
                m => m.PackageId == request.PackageId && m.CartId == cart.Id, cancellationToken);

            if (cartPackage == null)
                return NotFound<CartPackage>();

            if (cartPackage.Quantity <= request.Quantity)
            {
                _context.Remove(cartPackage);
            }
            else if(cartPackage.Quantity > request.Quantity)
            {
                cartPackage.Quantity -= request.Quantity;
                _context.Update(cartPackage.Quantity);
            }
            
            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}