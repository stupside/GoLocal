using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Enums;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Artisan.Application.Commands.Packages.UpdatePackageVisibility
{
    public class UpdatePackageVisibilityCommandHandler : AbstractRequestHandler<UpdatePackageVisibilityCommand>
    {
        private readonly Context _context;

        public UpdatePackageVisibilityCommandHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result> Handle(UpdatePackageVisibilityCommand request, CancellationToken cancellationToken)
        {
            Package package = await _context.Packages.SingleOrDefaultAsync(m =>
                m.Id == request.PackageId && m.ItemId == request.ItemId && m.Item.ShopId == request.ShopId, cancellationToken);

            if (package == null)
                return NotFound<Package>(request.PackageId);

            if (package.Visibility == request.Visibility)
                return Ok();

            package.Visibility = request.Visibility;
            
            _context.Packages.Update(package);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}