using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Identity.Application.Commands.Users.ResetPasswordConfirmation
{
    public class ResetPasswordConfirmationCommand : AbstractRequest
    {
        public string Email { get; init; }
        public string Token { get; init; }
        public string OldPassword { get; init; }
        public string NewPassword { get; init; }
        public string NewPasswordConfirmation { get; init; }

        public ResetPasswordConfirmationCommand()
        {
        }

        public ResetPasswordConfirmationCommand(string email, string token, string oldPassword, string newPassword, string newPasswordConfirmation)
        {
            Email = email;
            Token = token;
            OldPassword = oldPassword;
            NewPassword = newPassword;
            NewPasswordConfirmation = newPasswordConfirmation;
        }
    }
}