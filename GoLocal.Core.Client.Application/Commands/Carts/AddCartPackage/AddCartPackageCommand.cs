using GoLocal.Bus.Commons.Mediator;

namespace GoLocal.Core.Client.Application.Commands.Carts.AddCartPackage
{
    public class AddCartPackageCommand : AbstractRequest
    {
        public int ShopId { get; init; }
        public int PackageId { get; init; }
        public int ItemId { get; init; }
        public int Quantity { get; init; }
        
    }
}