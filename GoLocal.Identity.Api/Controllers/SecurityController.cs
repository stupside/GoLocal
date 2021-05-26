using System.Threading.Tasks;
using GoLocal.Identity.Api.Controllers.Commons;
using GoLocal.Identity.Application.Commands.Users.UpdateEmail;
using GoLocal.Identity.Application.Commands.Users.UpdateEmailConfirmation;
using GoLocal.Identity.Application.Commands.Users.UpdatePassword;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Identity.Api.Controllers
{
    [Authorize]
    [Route("/security")]
    public class SecurityController : ApiController
    {
        public SecurityController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPatch("password")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordCommand command)
            => await Handle(command);

        [HttpPatch("email")]
        public async Task<IActionResult> UpdateEmail(UpdateEmailCommand command)
            => await Handle(command.WithCallback("https://localhost:3000/security/email/confirmation"));

        [AllowAnonymous]
        [HttpPost("email/confirmation")]
        public async Task<IActionResult> UpdateEmailConfirmation(UpdateEmailConfirmationCommand command)
            => await Handle(command);
    }
}