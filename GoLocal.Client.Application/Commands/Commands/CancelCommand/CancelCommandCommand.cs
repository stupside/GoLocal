using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Client.Application.Commands.Commands.CancelCommand
{
    public class CancelCommandCommand : AbstractRequest
    {
        public string CommandId { get; init; }
    }
}