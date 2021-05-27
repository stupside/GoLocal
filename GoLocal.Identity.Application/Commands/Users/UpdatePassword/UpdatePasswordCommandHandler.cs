using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace GoLocal.Identity.Application.Commands.Users.UpdatePassword
{
    public class UpdatePasswordCommandHandler : AbstractRequestHandler<UpdatePasswordCommand>
    {
        private readonly UserManager<User> _manager;
        private readonly IUserAccessor<User> _user;

        public UpdatePasswordCommandHandler(IUserAccessor<User> user, UserManager<User> manager)
        {
            _user = user;
            _manager = manager;
        }

        public override async Task<Result> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            User user = await _user.GetUserAsync();
            
            IdentityResult result = await _manager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

            if (!result.Succeeded)
                return BadRequest(string.Join(',', result.Errors.SelectMany(m => m.Description)));
            
            return Ok();
        }
    }
}