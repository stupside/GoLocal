using System.Threading;
using System.Threading.Tasks;
using GoLocal.Identity.Domain.Entities;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using GoLocal.Shared.Mailing.Commons.Models;
using GoLocal.Shared.Mailing.Interfaces;
using MediatR;
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
            if (request.Password != request.PasswordConfirmation)
                return BadRequest("Passwords doesn't match");

            User user = new User(request.Email, request.Username);
            
            var result = await _user.CreateAsync(user, request.Password);
            
            var token = await _user.GenerateEmailConfirmationTokenAsync(user);
            await _email.SendAsync(new EmailMessage(user.Email, "Email Confirmation", 
                $@"Dear {user.UserName}, Thanks for your registering to our platform. Click on the link to complete your registration: {token}"), cancellationToken);

            if (result.Succeeded)
                return Ok(user.Id);

            //foreach (var error in result.Errors)
            //     ModelState.AddModelError(string.Empty, error.Description);
            
            return BadRequest("Something went wrong");
        }
    }
}