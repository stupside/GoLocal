using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Client.Application.Commands.Carts.DeleteCart
{
    public class DeleteCartCommandHandler : AbstractRequestHandler<DeleteCartCommand>
    {
        private readonly IUserAccessor<User> _accessor;
        private readonly Context _context;

        public DeleteCartCommandHandler(Context context, IUserAccessor<User> accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public override async Task<Result> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            User user = await _accessor.GetUserAsync();

            Cart cart = await _context.Carts.SingleOrDefaultAsync(
                m => m.ShopId == request.ShopId && m.UserId == user.Id, cancellationToken);

            if (cart == null)
                return NotFound<Cart>();

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Ok();
        }
    }
}