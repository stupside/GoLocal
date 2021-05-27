using GoLocal.Bus.Commons.Mediator;

namespace GoLocal.Identity.Application.Commands.Users.UpdatePassword
{
    public class UpdatePasswordCommand : AbstractRequest
    {
        public string OldPassword { get; init; }
        public string NewPassword { get; init; }
        public string NewPasswordConfirmation { get; init; }

        public UpdatePasswordCommand(string oldPassword, string newPassword, string newPasswordConfirmation)
        {
            OldPassword = oldPassword;
            NewPassword = newPassword;
            NewPasswordConfirmation = newPasswordConfirmation;
        }
    }
}