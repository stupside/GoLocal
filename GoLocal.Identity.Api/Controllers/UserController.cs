using System.Threading.Tasks;
using GoLocal.Identity.Api.Controllers.Commons;
using GoLocal.Identity.Application.Commands.Users.CreateUserConfirmation;
using GoLocal.Identity.Application.Commands.Users.UpdateEmail;
using GoLocal.Identity.Application.Commands.Users.UpdateEmailConfirmation;
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
            => await this.Handle(command);

        [HttpPatch("email")]
        public async Task<IActionResult> UpdateEmail(UpdateEmailCommand command)
            => await this.Handle(command.SetCallback(Url.Action("UpdateEmailConfirmation")));

        [HttpGet("confirmation/email")]
        public async Task<IActionResult> UpdateEmailConfirmation(string uid, string email, string token)
            => await this.Handle(new UpdateEmailConfirmationCommand(uid, email, token));
    }
}