using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Identity.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace GoLocal.Identity.Application.Commands.Users.UpdateAvatar
{
    public class UpdateAvatarCommandHandler : AbstractRequestHandler<UpdateAvatarCommand>
    {
        private readonly IUserAccessor<User> _accessor;

        public UpdateAvatarCommandHandler(IUserAccessor<User> accessor)
        {
            _accessor = accessor;
        }

        public override async Task<Result> Handle(UpdateAvatarCommand request, CancellationToken cancellationToken)
        {
            User user = await _accessor.GetUserAsync();

            IFormFile file = request.File;
            
            return Ok();
        }
    }
}