using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Identity.Application.Commands.Users.CreateUser
{
    public class CreateUserCommand: AbstractRequest<string>
    {
        public string Email { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
        public string PasswordConfirmation { get; init; }

        public CreateUserCommand()
        {
        }

        public CreateUserCommand(string email, string username, string password, string passwordConfirmation)
        {
            Email = email;
            Username = username;
            Password = password;
            PasswordConfirmation = passwordConfirmation;
        }
        
        public string Callback { get; private set; }
        public CreateUserCommand SetCallback(string url)
        {
            Callback = url;
            return this;
        }
    }
}