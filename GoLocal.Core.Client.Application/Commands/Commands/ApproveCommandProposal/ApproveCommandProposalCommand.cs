using GoLocal.Bus.Commons.Mediator;

namespace GoLocal.Core.Client.Application.Commands.Commands.ApproveCommandProposal
{
    public class ApproveCommandProposalCommand : AbstractRequest
    {
        public string CommandId { get; init; }
        public int CommandProposalId { get; init; }
    }
}