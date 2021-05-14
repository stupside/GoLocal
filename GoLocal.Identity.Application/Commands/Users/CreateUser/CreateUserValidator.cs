using FluentValidation;

namespace GoLocal.Identity.Application.Commands.Users.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(m => m.Email).NotEmpty().EmailAddress();
            RuleFor(m => m.Username).NotEmpty();
            RuleFor(m => m.Password).NotEmpty();
            RuleFor(m => m.Password).Equal(m => m.PasswordConfirmation);
        }
    }
}