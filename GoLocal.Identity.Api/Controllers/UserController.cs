using System.Threading.Tasks;
using GoLocal.Identity.Api.Controllers.Commons;
using GoLocal.Identity.Application.Commands.Users.ConfirmEmail;
using GoLocal.Identity.Application.Commands.Users.UpdateEmail;
using GoLocal.Identity.Application.Commands.Users.UpdatePassword;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Identity.Api.Controllers
{
    [Authorize]
    [Route("/users")]
    public class UserController : ApiController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPatch("password")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordCommand command)
            => await this.Send(command);

        [HttpPatch("email")]
        public async Task<IActionResult> UpdateEmail(UpdateEmailCommand command)
            => await this.Send(command);

        [HttpPost("{uid}/email/confirmation/{token}")]
        public async Task<IActionResult> ConfirmEmail(string uid, string token)
            => await this.Send(new ConfirmEmailCommand(token, uid));

    }
}