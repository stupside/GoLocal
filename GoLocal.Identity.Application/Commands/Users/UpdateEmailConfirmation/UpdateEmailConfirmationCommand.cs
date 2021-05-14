using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Identity.Application.Commands.Users.UpdateEmailConfirmation
{
    public class UpdateEmailConfirmationCommand : AbstractRequest
    {
        public string Email { get; init; }
        public string Token { get; init; }

        public UpdateEmailConfirmationCommand(string email, string token)
        {
            Email = email;
            Token = token;
        }
    }
}