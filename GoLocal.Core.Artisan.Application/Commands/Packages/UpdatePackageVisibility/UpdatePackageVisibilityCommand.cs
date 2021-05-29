using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Enums;

namespace GoLocal.Core.Artisan.Application.Commands.Packages.UpdatePackageVisibility
{
    public class UpdatePackageVisibilityCommand : AbstractRequest
    {
        public int ShopId { get; init; }
        public int ItemId { get; init; }
        public int PackageId { get; init; }
        public Visibility Visibility { get; init; }
    }
}