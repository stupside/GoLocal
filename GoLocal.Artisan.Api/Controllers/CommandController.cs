using System.Threading.Tasks;
using GoLocal.Artisan.Api.Controllers.Base;
using GoLocal.Artisan.Application.Commands.Commands.GenerateCommandInvoice;
using GoLocal.Artisan.Application.Queries.Commands.GetCommands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Artisan.Api.Controllers
{
    [Route("api/commands")]
    public class CommandController : ApiController
    {
        public CommandController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> Get(GetCommandsQuery query)
            => await Handle(query);

        [HttpPost("{cid}/invoices")]
        public async Task<IActionResult> GenerateInvoice(string cid, GenerateCommandInvoiceCommand command)
        {
            if (cid != command.CommandId)
                return BadRequest();
            
            return await Handle(command);
        }
    }
}