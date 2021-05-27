using GoLocal.Bus.Commons.Mediator;

namespace GoLocal.Identity.Application.Commands.Users.UpdateEmailConfirmation
{
    public class UpdateEmailConfirmationCommand : AbstractRequest
    {
        public string Uid { get; init; }
        public string Email { get; init; }
        public string Token { get; init; }
    }
}