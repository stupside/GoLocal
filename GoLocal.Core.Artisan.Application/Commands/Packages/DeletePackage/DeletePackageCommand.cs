using GoLocal.Bus.Commons.Mediator;

namespace GoLocal.Core.Artisan.Application.Commands.Packages.DeletePackage
{
    public class DeletePackageCommand : AbstractRequest
    {
        public int ShopId { get; init; }
        public int ItemId { get; init; }
        public int PackageId { get; init; }

        public DeletePackageCommand(int shopId, int itemId, int packageId)
        {
            ShopId = shopId;
            ItemId = itemId;
            PackageId = packageId;
        }
    }
}