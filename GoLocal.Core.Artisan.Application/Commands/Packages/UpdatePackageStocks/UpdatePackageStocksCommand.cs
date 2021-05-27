using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Artisan.Application.Commands.Packages.UpdatePackageStocks
{
    [AuthorizedEntity(typeof(Shop))]
    public class UpdatePackageStocksCommand : AbstractRequest
    {
        [AuthorizedEntityId]
        public int ShopId { get; init; }
        public int ItemId { get; init; }
        public int PackageId { get; init; }
        public int Stocks { get; init; }
    }
}