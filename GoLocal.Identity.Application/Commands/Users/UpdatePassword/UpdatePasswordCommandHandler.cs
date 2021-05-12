using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Identity.Application.Commons.Accessor;
using GoLocal.Identity.Domain.Entities;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GoLocal.Identity.Application.Commands.Users.UpdatePassword
{
    public class UpdatePasswordCommandHandler : AbstractRequestHandler<UpdatePasswordCommand>
    {
        private readonly IUserAccessor _user;

        public UpdatePasswordCommandHandler(IUserAccessor user)
        {
            _user = user;
        }

        public override async Task<Result<Unit>> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            User user = await _user.GetUserAsync();

            if (request.Password == request.Current)
                return BadRequest("New password shouldn't be equal");
            
            IdentityResult result = await _user.Manager.ChangePasswordAsync(user, request.Current, request.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors.First().Description);
            
            return Ok();
        }
    }
}