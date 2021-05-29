using System.Threading;
using System.Threading.Tasks;
using System.Web;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Identity.Domain.Entities;
using GoLocal.Shared.Mailing.Commons.Models;
using GoLocal.Shared.Mailing.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GoLocal.Identity.Application.Commands.Users.UpdateEmail
{
    public class UpdateEmailCommandHandler : AbstractRequestHandler<UpdateEmailCommand>
    {
        private readonly UserManager<User> _manager;
        private readonly IUserAccessor<User> _user;
        private readonly IEmailService _email;
        public UpdateEmailCommandHandler(IUserAccessor<User> user, IEmailService email, UserManager<User> manager)
        {
            _user = user;
            _email = email;
            _manager = manager;
        }

        public override async Task<Result> Handle(UpdateEmailCommand request, CancellationToken cancellationToken)
        {
            User user = await _user.GetUserAsync();

            string token = await _manager.GenerateChangeEmailTokenAsync(user, request.Email);
            token = HttpUtility.UrlEncode(token);
            
            await _email.SendAsync(new EmailMessage(request.Email, "Email reset", 
                $"Welcome {user.UserName},\nYou tried to change your email.\nTo complete, please click : {request.Callback}?token={token}&uid={user.Id}&email={HttpUtility.UrlEncode(request.Email)}"), cancellationToken);

            return Ok();
        }
    }
}