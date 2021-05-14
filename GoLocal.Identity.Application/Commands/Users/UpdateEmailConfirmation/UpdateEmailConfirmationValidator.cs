using FluentValidation;

namespace GoLocal.Identity.Application.Commands.Users.UpdateEmailConfirmation
{
    public class UpdateEmailConfirmationValidator : AbstractValidator<UpdateEmailConfirmationCommand>
    {
        public UpdateEmailConfirmationValidator()
        {
            RuleFor(m => m.Token).NotEmpty();
            RuleFor(m => m.Email).NotEmpty().EmailAddress();
        }
    }
}