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
    [Route("api/shops/{sid:int}/commands")]
    public class CommandController : ApiController
    {
        public CommandController(IMediator mediator) : base(mediator)
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [HttpGet("{cid}")]
        public async Task<IActionResult> Get(int sid, string cid)
            => await Handle(new GetCommandQuery(sid, cid));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> Get(int sid, GetCommandsQuery query)
            => await Handle(query);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("accept")]
        public async Task<IActionResult> Accept(int sid, AcceptCommandCommand command)
        {
            if (sid != command.ShopId)
                return BadRequest();
            
            return await Handle(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("invoice")]
        public async Task<IActionResult> GenerateInvoice(int sid, GenerateCommandInvoiceCommand command)
        {
            if (sid != command.ShopId)
                return BadRequest();
            
            return await Handle(command);
        }
    }
}