using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Identity.Application.Commands.Users.CreateUserConfirmation
{
    public class CreateUserConfirmationCommand : AbstractRequest
    {
        public string Token { get; }
        public string Uid { get; }

        public CreateUserConfirmationCommand(string token, string uid)
        {
            Token = token;
            Uid = uid;
        }
    }
}