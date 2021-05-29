using GoLocal.Bus.Commons.Mediator;

namespace GoLocal.Core.Client.Application.Commands.Commands.CreateCommand
{
    public class CreateCommandCommand : AbstractRequest<string>
    {
        public int ShopId { get; init; }
        public int ServiceId { get; init; }
        public int PackageId { get; init; }
        public float Price { get; init; }
        public string Specifications { get; init; }
    }
}