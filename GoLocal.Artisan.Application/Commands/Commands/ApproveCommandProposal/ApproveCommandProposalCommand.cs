using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Artisan.Application.Commands.Commands.ApproveCommandProposal
{
    public class ApproveCommandProposalCommand : AbstractRequest
    {
        public string CommandId { get; init; }
        public int CommandProposalId { get; init; }
    }
}