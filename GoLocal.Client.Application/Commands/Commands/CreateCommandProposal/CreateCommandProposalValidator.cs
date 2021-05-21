using FluentValidation;

namespace GoLocal.Client.Application.Commands.Commands.CreateCommandProposal
{
    public class CreateCommandProposalValidator : AbstractValidator<CreateCommandProposalCommand>
    {
        public CreateCommandProposalValidator()
        {
            RuleFor(m => m.Specification).NotEmpty();
            RuleFor(m => m.CommandId).NotEmpty();
            RuleFor(m => m.Price).NotEmpty().InclusiveBetween(1, 10000);
        }
    }
}