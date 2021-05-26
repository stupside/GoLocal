using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Identity.Application.Commands.Users.ResetPassword
{
    public class ResetPasswordCommand : AbstractRequest
    {
        public string Email { get; init; }
        public string Callback { get; private set; }
        public ResetPasswordCommand WithCallback(string url)
        {
            Callback = url;
            return this;
        }
    }
}