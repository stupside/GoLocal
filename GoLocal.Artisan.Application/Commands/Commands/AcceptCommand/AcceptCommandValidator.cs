using FluentValidation;

namespace GoLocal.Artisan.Application.Commands.Commands.AcceptCommand
{
    public class RejectCommandValidator : AbstractValidator<AcceptCommandCommand>
    {
        public RejectCommandValidator()
        {
            RuleFor(m => m.CommandId).NotEmpty();
        }
    }
}