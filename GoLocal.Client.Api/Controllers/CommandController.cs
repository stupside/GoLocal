using System.Threading.Tasks;
using GoLocal.Client.Api.Controllers.Base;
using GoLocal.Client.Application.Commands.Commands.CreateCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Client.Api.Controllers
{
    [Route("api/commands")]
    public class CommandController : ApiController
    {
        public CommandController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPut]
        public async Task<IActionResult> Create(CreateCommandCommand command)
            => await Handle(command);
    }
}