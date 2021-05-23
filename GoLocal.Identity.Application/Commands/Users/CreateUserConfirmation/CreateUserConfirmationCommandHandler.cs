using System.Threading;
using System.Threading.Tasks;
using GoLocal.Identity.Domain.Entities;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using Microsoft.AspNetCore.Identity;

namespace GoLocal.Identity.Application.Commands.Users.CreateUserConfirmation
{
    public class CreateUserConfirmationCommandHandler : AbstractRequestHandler<CreateUserConfirmationCommand>
    {        
        private readonly UserManager<User> _user;
        
        public CreateUserConfirmationCommandHandler(UserManager<User> user)
        {
            _user = user;
        }

        public override async Task<Result> Handle(CreateUserConfirmationCommand request, CancellationToken cancellationToken)
        {
            var user = await _user.FindByIdAsync(request.Uid);
            if (user == null)
                return BadRequest("Email confirmation failed");

            if (user.EmailConfirmed)
                return BadRequest("Email confirmation failed");
            
            var result = await _user.ConfirmEmailAsync(user, request.Token);

            return !result.Succeeded ? BadRequest("Email confirmation failed") : Ok();
        }
    }
}