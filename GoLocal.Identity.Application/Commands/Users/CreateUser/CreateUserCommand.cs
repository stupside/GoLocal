using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Identity.Application.Commands.Users.CreateUser
{
    public class CreateUserCommand: AbstractRequest<string>
    {
        public string Email { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
        public string PasswordConfirmation { get; init; }
        public string Callback { get; private set; }
        public CreateUserCommand WithCallback(string url)
        {
            Callback = url;
            return this;
        }
    }
}