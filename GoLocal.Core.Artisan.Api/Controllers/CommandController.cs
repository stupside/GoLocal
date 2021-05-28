using System.Threading.Tasks;
using GoLocal.Core.Artisan.Api.Controllers.Base;
using GoLocal.Core.Artisan.Application.Commands.Commands.AcceptCommand;
using GoLocal.Core.Artisan.Application.Commands.Commands.GenerateCommandInvoice;
using GoLocal.Core.Artisan.Application.Queries.Commands.GetCommand;
using GoLocal.Core.Artisan.Application.Queries.Commands.GetCommands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Core.Artisan.Api.Controllers
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
        /// <param name="sid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [HttpGet("shops/{sid:int}/users/{uid}")]
        public async Task<IActionResult> Get(int sid, string uid)
            => await Handle(new GetCommandQuery(sid, uid));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> Get(GetCommandsQuery query)
            => await Handle(query);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("{cid}")]
        public async Task<IActionResult> Accept(string cid, AcceptCommandCommand command)
        {
            if (cid != command.CommandId)
                return BadRequest();
            
            return await Handle(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("{cid}/invoices")]
        public async Task<IActionResult> GenerateInvoice(string cid, GenerateCommandInvoiceCommand command)
        {
            if (cid != command.CommandId)
                return BadRequest();
            
            return await Handle(command);
        }
    }
}