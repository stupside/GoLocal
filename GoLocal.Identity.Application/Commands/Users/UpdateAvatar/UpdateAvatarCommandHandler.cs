using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Identity.Application.Commons.Helpers;
using GoLocal.Identity.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace GoLocal.Identity.Application.Commands.Users.UpdateAvatar
{
    public class UpdateAvatarCommandHandler : AbstractRequestHandler<UpdateAvatarCommand>
    {
        private readonly UserManager<User> _manager;
        private readonly IUserAccessor<User> _accessor;

        public UpdateAvatarCommandHandler(IUserAccessor<User> accessor, UserManager<User> manager)
        {
            _accessor = accessor;
            _manager = manager;
        }

        public override async Task<Result> Handle(UpdateAvatarCommand request, CancellationToken cancellationToken)
        {
            User user = await _accessor.GetUserAsync();

            user.Avatar = await request.File.ResizeAsync();
            await _manager.UpdateAsync(user);
            
            return Ok();
        }
    }
}