using GoLocal.Bus.Commons.Mediator;
using Microsoft.AspNetCore.Http;

namespace GoLocal.Identity.Application.Commands.Users.UpdateAvatar
{
    public class UpdateAvatarCommand : AbstractRequest
    {
        public IFormFile File { get; init; }

        public UpdateAvatarCommand(IFormFile file)
        {
            File = file;
        }
    }
}