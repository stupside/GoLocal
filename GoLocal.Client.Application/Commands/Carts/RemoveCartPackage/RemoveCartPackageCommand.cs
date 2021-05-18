using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Client.Application.Commands.Carts.RemoveCartPackage
{
    public class RemoveCartPackageCommand : AbstractRequest
    {
        public int ShopId { get; init; } 
        public int PackageId { get; init; }
        public int Quantity { get; init; }
    }
}