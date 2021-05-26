using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Identity.Application.Commands.Users.ResetPasswordConfirmation
{
    public class ResetPasswordConfirmationCommand : AbstractRequest
    {
        public string Token { get; init; }
        public string Uid { get; init; }
        public string Password { get; init; }
        public string PasswordConfirmation { get; init; }
    }
}