using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Identity.Application.Commands.Users.ResetPasswordConfirmation
{
    public class ResetPasswordConfirmationCommand : AbstractRequest
    {
        public string OldPassword { get; init; }
        public string NewPassword { get; init; }
        public string NewPasswordConfirmation { get; init; }

        public ResetPasswordConfirmationCommand()
        {
        }

        public string Email { get; private set; }
        public ResetPasswordConfirmationCommand WithEmail(string email)
        {
            Email = email;
            return this;
        }
        
        public string Token { get; private set; }
        public ResetPasswordConfirmationCommand WithToken(string token)
        {
            Token = token;
            return this;
        }
    }
}