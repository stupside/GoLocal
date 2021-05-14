using FluentValidation;

namespace GoLocal.Identity.Application.Commands.Users.UpdateEmail
{
    public class UpdateEmailValidator : AbstractValidator<UpdateEmailCommand>
    {
        public UpdateEmailValidator()
        {
            RuleFor(m => m.Email).NotEmpty().EmailAddress();
        }
    }
}