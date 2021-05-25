using System.Net;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Identity.Domain.Entities;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using GoLocal.Shared.Mailing.Commons.Models;
using GoLocal.Shared.Mailing.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GoLocal.Identity.Application.Commands.Users.ResetPassword
{
    public class ResetPasswordCommandHandler : AbstractRequestHandler<ResetPasswordCommand>
    {
        private readonly UserManager<User> _user;
        private readonly IEmailService _email;

        public ResetPasswordCommandHandler(UserManager<User> user, IEmailService email)
        {
            _user = user;
            _email = email;
        }

        public override async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            User user = await _user.FindByEmailAsync(request.Email);
            if (user == null)
                return Ok();

            var token = await _user.GeneratePasswordResetTokenAsync(user);
            token = WebUtility.UrlEncode(token);
            
            await _email.SendAsync(new EmailMessage(user.Email, "Password reset", 
                $"Welcome {user.UserName},\nYou tried to reset your password.\nTo complete, please click : {request.Callback}?token={token}"), cancellationToken);

            return Ok();
        }
    }
}