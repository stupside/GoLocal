using FluentValidation;

namespace GoLocal.Artisan.Application.Commands.Commands.CreateCommandProposal
{
    public class CreateProposalSpecificationValidator : AbstractValidator<CreateCommandProposalCommand>
    {
        public CreateProposalSpecificationValidator()
        {
            RuleFor(m => m.Specification).NotEmpty();
            RuleFor(m => m.CommandId).NotEmpty();
            RuleFor(m => m.Price).NotEmpty().InclusiveBetween(1, 10000);
        }
    }
}