using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Client.Application.Commands.Commands.CreateCommand
{
    public class CreateCommandCommand : AbstractRequest<string>
    {
        public int ServiceId { get; init; }
        public int PackageId { get; init; }
        public float Price { get; init; }
        public string Specifications { get; init; }
    }
}