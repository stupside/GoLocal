using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Identity.Application.Commands.Users.ResetPassword
{
    public class ResetPasswordCommand : AbstractRequest
    {
        public string Email { get; init; }

        public ResetPasswordCommand(string email)
        {
            Email = email;
        }
        
        public string Callback { get; private set; }
        public ResetPasswordCommand SetCallback(string url)
        {
            Callback = url;
            return this;
        }
    }
}