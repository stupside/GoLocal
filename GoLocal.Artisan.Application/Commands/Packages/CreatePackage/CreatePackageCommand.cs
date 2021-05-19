using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Artisan.Application.Commands.Packages.CreatePackage
{
    public class CreatePackageCommand : AbstractRequest<int>
    {
        public int ShopId { get; init; }
        public int ItemId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public float Price { get; init; }
        public int Stocks { get; init; }
    }
}