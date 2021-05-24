using System.Threading.Tasks;
using GoLocal.Identity.Api.Controllers.Commons;
using GoLocal.Identity.Application.Commands.Users.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Identity.Api.Controllers
{
    [Authorize]
    [Microsoft.AspNetCore.Components.Route("/account")]
    public class AccountController : ApiController
    {
        public AccountController(IMediator mediator) : base(mediator)
        {
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserCommand command)
            => await this.Handle(command);
    }
}