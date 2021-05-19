using System.Threading;
using System.Threading.Tasks;
using GoLocal.Domain.Entities;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Artisan.Application.Commands.Packages.UpdatePackageStocks
{
    public class UpdatePackageStocksCommandHandler : AbstractRequestHandler<UpdatePackageStocksCommand>
    {
        private readonly Context _context;

        public UpdatePackageStocksCommandHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result> Handle(UpdatePackageStocksCommand request, CancellationToken cancellationToken)
        {
            Package package = await _context.Packages.SingleOrDefaultAsync(
                m => m.Id == request.PackageId && m.ItemId == request.ItemId && m.Item.ShopId == request.ShopId,
                cancellationToken);

            if (package == null)
                return NotFound<Package>(request.PackageId);

            package.Stocks = request.Stocks;

            _context.Packages.Update(package);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Ok();
        }
    }
}