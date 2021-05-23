using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Identity.Application.Commands.Users.UpdateEmail
{
    public class UpdateEmailCommand : AbstractRequest
    {
        public string Email { get; init; }

        public UpdateEmailCommand(string email)
        {
            Email = email;
        }
        
        public string Callback { get; private set; }
        public UpdateEmailCommand SetCallback(string url)
        {
            Callback = url;
            return this;
        }
    }
}