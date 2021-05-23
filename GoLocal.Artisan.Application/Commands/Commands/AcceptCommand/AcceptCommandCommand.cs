using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Artisan.Application.Commands.Commands.AcceptCommand
{
    public class AcceptCommandCommand : AbstractRequest
    {
        public string CommandId { get; init; }
        public bool Accept { get; init; }
    }
}