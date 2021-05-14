using FluentValidation;

namespace GoLocal.Identity.Application.Commands.Users.ResetPasswordConfirmation
{
    public class ResetPasswordConfirmationValidator : AbstractValidator<ResetPasswordConfirmationCommand>
    {
        public ResetPasswordConfirmationValidator()
        {
            RuleFor(m => m.Email).NotEmpty().EmailAddress();
            RuleFor(m => m.Token).NotEmpty();
            
            RuleFor(m => m.OldPassword).NotEmpty();
            RuleFor(m => m.NewPassword).NotEmpty();
            RuleFor(m => m.NewPassword).Equal(m => m.NewPasswordConfirmation);
        }
    }
}