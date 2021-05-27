using GoLocal.Bus.Commons.Mediator;

namespace GoLocal.Core.Client.Application.Commands.Commands.CancelCommand
{
    public class CancelCommandCommand : AbstractRequest
    {
        public string CommandId { get; init; }
    }
}