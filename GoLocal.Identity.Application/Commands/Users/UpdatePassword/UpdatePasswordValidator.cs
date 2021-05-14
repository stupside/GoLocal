using FluentValidation;

namespace GoLocal.Identity.Application.Commands.Users.UpdatePassword
{
    public class UpdatePasswordValidator : AbstractValidator<UpdatePasswordCommand>
    {
        public UpdatePasswordValidator()
        {
            RuleFor(m => m.OldPassword).NotEmpty();
            RuleFor(m => m.NewPassword).NotEmpty();
            RuleFor(m => m.NewPassword).Equal(m => m.NewPasswordConfirmation);
        }
    }
}