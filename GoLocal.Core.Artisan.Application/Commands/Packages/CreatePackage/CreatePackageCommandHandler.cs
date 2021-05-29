using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Entities.Abstracts;
using GoLocal.Core.Domain.Enums;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Artisan.Application.Commands.Packages.CreatePackage
{
    public class CreatePackageCommandHandler : AbstractRequestHandler<CreatePackageCommand, int>
    {
        private readonly Context _context;

        public CreatePackageCommandHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result<int>> Handle(CreatePackageCommand request, CancellationToken cancellationToken)
        {
            Item item = await _context.Items.SingleOrDefaultAsync(m => m.Id == request.ItemId && m.ShopId == request.ShopId, cancellationToken);
            if (item == null)
                return NotFound<Item>(request.ItemId);
            
            if(await _context.Packages.AnyAsync(m => m.ItemId == request.ItemId && m.Name == request.Name, cancellationToken))
                return BadRequest($"A package named {request.Name} already exists");

            Package package = new Package(item, request.Name, request.Description, request.Price, request.Stocks);

            await _context.Packages.AddAsync(package, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok(package.Id);
        }
    }
}