using FluentValidation;

namespace GoLocal.Artisan.Application.Commands.Commands.ApproveCommandProposal
{
    public class ApproveCommandProposalValidator : AbstractValidator<ApproveCommandProposalCommand>
    {
        public ApproveCommandProposalValidator()
        {
            RuleFor(m => m.CommandId).NotEmpty();
            RuleFor(m => m.CommandProposalId).NotEmpty();
        }
    }
}