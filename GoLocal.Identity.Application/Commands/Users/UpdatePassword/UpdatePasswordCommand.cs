using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Identity.Application.Commands.Users.UpdatePassword
{
    public class UpdatePasswordCommand : AbstractRequest
    {
        public string Current { get; init; }
        public string Password { get; init; }

        public UpdatePasswordCommand(string current, string password)
        {
            Current = current;
            Password = password;
        }
    }
}