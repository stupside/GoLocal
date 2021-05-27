using GoLocal.Bus.Commons.Mediator;

namespace GoLocal.Core.Client.Application.Commands.Carts.RemoveCartPackage
{
    public class RemoveCartPackageCommand : AbstractRequest
    {
        public int ShopId { get; init; } 
        public int PackageId { get; init; }
        public int Quantity { get; init; }
    }
}