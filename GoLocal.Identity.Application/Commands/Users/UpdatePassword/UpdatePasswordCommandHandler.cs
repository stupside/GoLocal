using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Identity.Application.Commons.Accessor;
using GoLocal.Identity.Domain.Entities;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
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

        public override async Task<Result> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            User user = await _user.GetUserAsync();
            
            IdentityResult result = await _user.Manager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

            if (!result.Succeeded)
                return BadRequest(string.Join(',', result.Errors.SelectMany(m => m.Description)));
            
            return Ok();
        }
    }
}