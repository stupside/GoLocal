using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Artisan.Application.Commands.Packages.UpdatePackageStocks
{
    public class UpdatePackageStocksCommand : AbstractRequest
    {
        public int ShopId { get; init; }
        public int ItemId { get; init; }
        public int PackageId { get; init; }
        public int Stocks { get; init; }
    }
}