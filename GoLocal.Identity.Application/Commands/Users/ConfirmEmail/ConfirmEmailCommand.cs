using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Identity.Application.Commands.Users.ConfirmEmail
{
    public class ConfirmEmailCommand : AbstractRequest
    {
        public string Token { get; init; }
        public string Uid { get; init; }

        public ConfirmEmailCommand(string token, string uid)
        {
            Token = token;
            Uid = uid;
        }
    }
}