using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Identity.Application.Commands.Users.CreateUserConfirmation
{
    public class CreateUserConfirmationCommand : AbstractRequest
    {
        public string Token { get; init; }
        public string Uid { get; init; }

        public CreateUserConfirmationCommand(string token, string uid)
        {
            Token = token;
            Uid = uid;
        }
    }
}