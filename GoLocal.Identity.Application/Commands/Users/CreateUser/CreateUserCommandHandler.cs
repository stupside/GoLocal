using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Identity.Domain.Entities;
using GoLocal.Shared.Mailing.Commons.Models;
using GoLocal.Shared.Mailing.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GoLocal.Identity.Application.Commands.Users.CreateUser
{
    public class CreateUserCommandHandler: AbstractRequestHandler<CreateUserCommand, string>
    {
        private readonly UserManager<User> _user;
        private readonly IEmailService _email;

        public CreateUserCommandHandler(UserManager<User> user, IEmailService email)
        {
            _user = user;
            _email = email;
        }

        public override async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = new User(request.Email, request.Username);
            
            var result = await _user.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return BadRequest(string.Join(',', result.Errors.Select(m => m.Description)));
            
            var token = await _user.GenerateEmailConfirmationTokenAsync(user);
            token = HttpUtility.UrlEncode(token);

            await _email.SendAsync(new EmailMessage(user.Email, "Email Confirmation", 
                $"Welcome {user.UserName},\nTo complete your registration, please click : {request.Callback}?token={token}&uid={user.Id}"), cancellationToken);
            
            return Ok(user.Id);
        }
    }
}