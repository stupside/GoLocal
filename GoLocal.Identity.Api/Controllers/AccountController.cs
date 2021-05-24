using System.Threading.Tasks;
using GoLocal.Identity.Api.Controllers.Commons;
using GoLocal.Identity.Application.Commands.Users.CreateUser;
using GoLocal.Identity.Application.Commands.Users.CreateUserConfirmation;
using GoLocal.Identity.Application.Commands.Users.ResetPassword;
using GoLocal.Identity.Application.Commands.Users.ResetPasswordConfirmation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Identity.Api.Controllers
{
    [Route("/account")]
    public class AccountController : ApiController
    {
        public AccountController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserCommand command)
            => await this.Handle(command.SetCallback(Url.ActionLink("RegisterConfirmation")));

        [HttpGet("register/confirmation")]
        [HttpPost("register/confirmation")]
        public async Task<IActionResult> RegisterConfirmation(string token, string uid)
            => await Handle(new CreateUserConfirmationCommand(token, uid));
                
        [HttpPost("password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
            => await Handle(command.SetCallback(Url.ActionLink("ResetPasswordConfirmation")));

        [HttpPost("password/confirmation")]
        public async Task<IActionResult> ResetPasswordConfirmation(string token, string email, ResetPasswordConfirmationCommand command)
            => await Handle(command.WithToken(token).WithEmail(email));
    }
}