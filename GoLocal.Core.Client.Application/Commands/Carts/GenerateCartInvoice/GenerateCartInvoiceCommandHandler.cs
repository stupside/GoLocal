using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Client.Application.Commands.Carts.GenerateCartInvoice
{
    public class GenerateCartInvoiceCommandHandler : AbstractRequestHandler<GenerateCartInvoiceCommand, int>
    {
        private readonly Context _context;
        private readonly IUserAccessor<User> _accessor;

        public GenerateCartInvoiceCommandHandler(IUserAccessor<User> accessor, Context context)
        {
            _accessor = accessor;
            _context = context;
        }

        public override async Task<Result<int>> Handle(GenerateCartInvoiceCommand request, CancellationToken cancellationToken)
        {
            User user = await _accessor.GetUserAsync();

            Cart cart = await _context.Carts
                .Include(m => m.CartPackages).ThenInclude(m => m.Package)
                .SingleOrDefaultAsync(m => m.ShopId == request.ShopId && m.UserId == user.Id, cancellationToken);

            if (cart == null)
                return NotFound<Cart>();

            foreach (CartPackage element in cart.CartPackages)
            {
                if (element.Price - element.Package.Price != 0)
                    return BadRequest($"The price of the package named {element.Package.Name} changed");

                if(element.Package.Stocks - element.Quantity < 0)
                    return BadRequest($"Please update the quantity for the package named {element.Package.Name}. You requested {element.Quantity} when only {element.Package.Stocks} left");
            }

            Invoice invoice = new Invoice(cart);
            await _context.Invoices.AddAsync(invoice, cancellationToken);
            _context.Carts.Remove(cart);

            await _context.SaveChangesAsync(cancellationToken);
            return Ok(invoice.Id);
        }
    }
}