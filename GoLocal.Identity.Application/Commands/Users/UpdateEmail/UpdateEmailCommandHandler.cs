using System.Net;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Identity.Application.Commons.Accessor;
using GoLocal.Identity.Domain.Entities;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using GoLocal.Shared.Mailing.Commons.Models;
using GoLocal.Shared.Mailing.Interfaces;

namespace GoLocal.Identity.Application.Commands.Users.UpdateEmail
{
    public class UpdateEmailCommandHandler : AbstractRequestHandler<UpdateEmailCommand>
    {
        private readonly IUserAccessor _user;
        private readonly IEmailService _email;
        public UpdateEmailCommandHandler(IUserAccessor user, IEmailService email)
        {
            _user = user;
            _email = email;
        }

        public override async Task<Result> Handle(UpdateEmailCommand request, CancellationToken cancellationToken)
        {
            User user = await _user.GetUserAsync();

            string token = await _user.Manager.GenerateChangeEmailTokenAsync(user, request.Email);
            token = WebUtility.UrlEncode(token);
            
            await _email.SendAsync(new EmailMessage(user.Email, "Email reset", 
                $"Welcome {user.UserName},\nYou tried to change your email.\nTo complete, please click : {request.Callback}?token={token}&email={request.Email}"), cancellationToken);

            await _user.Manager.UpdateSecurityStampAsync(user);

            return Ok();
        }
    }
}