using FluentValidation;

namespace GoLocal.Artisan.Application.Commands.Commands.RejectCommand
{
    public class RejectCommandValidator : AbstractValidator<RejectCommandCommand>
    {
        public RejectCommandValidator()
        {
            RuleFor(m => m.CommandId).NotEmpty();
        }
    }
}