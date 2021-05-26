using FluentValidation;

namespace GoLocal.Identity.Application.Commands.Users.ResetPasswordConfirmation
{
    public class ResetPasswordConfirmationValidator : AbstractValidator<ResetPasswordConfirmationCommand>
    {
        public ResetPasswordConfirmationValidator()
        {
            RuleFor(m => m.Token).NotEmpty();
            
            RuleFor(m => m.Uid).NotEmpty();
            RuleFor(m => m.Password).NotEmpty();
            RuleFor(m => m.Password).Equal(m => m.PasswordConfirmation);
        }
    }
}