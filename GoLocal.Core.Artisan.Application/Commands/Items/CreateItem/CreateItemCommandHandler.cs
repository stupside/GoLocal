using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Entities.Abstracts;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Artisan.Application.Commands.Items.CreateItem
{
    public class CreateItemCommandHandler : AbstractRequestHandler<CreateItemCommand, int>
    {
        private readonly Context _context;

        public CreateItemCommandHandler(Context context)
        {
            _context = context;
        }
        
        public override async Task<Result<int>> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            Shop shop = await _context.Shops.SingleOrDefaultAsync(m => m.Id == request.ShopId, cancellationToken);
            if (shop == null)
                return NotFound<Shop>(request.ShopId);

            bool nameTaken = await _context.Items.AnyAsync(m => m.Name == request.Name && m.ShopId == request.ShopId, cancellationToken);
            if (nameTaken)
                return BadRequest($"An item named {request.Name} already exists");

            Item item;
            if (request.Type == typeof(Service))
            {
                item = new Service(shop, request.Name, request.Description);
            }
            else
            {
                item = new Product(shop, request.Name, request.Description);
            }
            
            await _context.Items.AddAsync(item, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok(item.Id);
        }
    }
}