using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Client.Application.Commands.Commands.CreateCommandProposal
{
    public class CreateCommandProposalCommand : AbstractRequest<int>
    {
        public string CommandId { get; init; }
        public string Specification { get; init; }
        public float Price { get; init; }
    }
}