using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Artisan.Application.Commands.Commands.RejectCommand
{
    public class RejectCommandCommand : AbstractRequest
    {
        public string CommandId { get; init; }
    }
}