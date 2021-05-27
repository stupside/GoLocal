using FluentValidation;

namespace GoLocal.Core.Client.Application.Commands.Commands.CancelCommand
{
    public class CancelCommandValidator : AbstractValidator<CancelCommandCommand>
    {
        public CancelCommandValidator()
        {
            RuleFor(m => m.CommandId).NotEmpty();
        }
    }
}