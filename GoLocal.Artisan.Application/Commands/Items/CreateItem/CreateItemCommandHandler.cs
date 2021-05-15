using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Domain.Entities;
using GoLocal.Domain.Entities.Abstracts;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Artisan.Application.Commands.Items.CreateItem
{
    public class CreateItemCommandHandler : AbstractRequestHandler<CreateItemCommand, string>
    {
        private readonly Context _context;

        public CreateItemCommandHandler(Context context)
        {
            _context = context;
        }
        
        public override async Task<Result<string>> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            Shop shop = await _context.Shops.SingleOrDefaultAsync(m => m.Id == request.ShopId, cancellationToken);
            if (shop == null)
                return NotFound<Shop>(request.ShopId);

            bool nameTaken = await _context.Items.AnyAsync(m => m.Name == request.Name && m.ShopId == request.ShopId, cancellationToken);
            if (nameTaken)
                return BadRequest($"An item named {request.Name} already exists");
            
            //TODO: Null Id
            if (request.Type == typeof(Service))
            {
                Service service = new Service(shop, request.Name, request.Description);
                await _context.Items.AddAsync(service, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                
                return Ok(service.Id);
            }else
            {
                Product product = new Product(shop, request.Name, request.Description);
                await _context.Items.AddAsync(product, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Ok(product.Id);
            }
        }
    }
}