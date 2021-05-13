using System.Threading;
using System.Threading.Tasks;
using GoLocal.Identity.Domain.Entities;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GoLocal.Identity.Application.Commands.Users.ConfirmEmail
{
    public class ConfirmEmailCommandHandler: AbstractRequestHandler<ConfirmEmailCommand>
    {        
        private readonly UserManager<User> _user;
        
        public ConfirmEmailCommandHandler(UserManager<User> user)
        {
            _user = user;
        }

        public override async Task<Result<Unit>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _user.FindByIdAsync(request.Uid);
            var result = await _user.ConfirmEmailAsync(user, request.Token);

            if (result.Succeeded)
                return Ok();

            return BadRequest("Email confirmation failed");
        }
    }
}