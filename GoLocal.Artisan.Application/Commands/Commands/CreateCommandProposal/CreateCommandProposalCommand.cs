using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Artisan.Application.Commands.Commands.CreateCommandProposal
{
    public class CreateCommandProposalCommand : AbstractRequest<int>
    {
        public string CommandId { get; init; }
        public float Price { get; init; }
        public string Specification { get; init; }
    }
}