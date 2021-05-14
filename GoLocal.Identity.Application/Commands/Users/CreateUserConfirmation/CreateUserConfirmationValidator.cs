using FluentValidation;

namespace GoLocal.Identity.Application.Commands.Users.CreateUserConfirmation
{
    public class CreateUserConfirmationValidator : AbstractValidator<CreateUserConfirmationCommand>
    {
        public CreateUserConfirmationValidator()
        {
            RuleFor(m => m.Token).NotEmpty();
            RuleFor(m => m.Uid).NotEmpty();
        }
    }
}