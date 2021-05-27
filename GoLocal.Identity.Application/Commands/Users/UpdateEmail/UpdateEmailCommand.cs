using GoLocal.Bus.Commons.Mediator;

namespace GoLocal.Identity.Application.Commands.Users.UpdateEmail
{
    public class UpdateEmailCommand : AbstractRequest
    {
        public string Email { get; init; }
        public string Callback { get; private set; }
        public UpdateEmailCommand WithCallback(string url)
        {
            Callback = url;
            return this;
        }
    }
}