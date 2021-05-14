using FluentValidation;

namespace GoLocal.Identity.Application.Commands.Users.ResetPassword
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordValidator()
        {
            RuleFor(m => m.Email).NotEmpty().EmailAddress();
        }
    }
}