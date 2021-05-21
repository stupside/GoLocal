using System.Threading.Tasks;
using GoLocal.Artisan.Api.Controllers.Base;
using GoLocal.Artisan.Application.Commands.Commands.ApproveCommandProposal;
using GoLocal.Artisan.Application.Commands.Commands.CreateCommandProposal;
using GoLocal.Artisan.Application.Commands.Commands.GenerateCommandInvoice;
using GoLocal.Artisan.Application.Queries.Commands.GetCommands;
using MediatR;
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
        [HttpPost]
        public async Task<IActionResult> Get(GetCommandsQuery query)
            => await Handle(query);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{cid}/proposals")]
        public async Task<IActionResult> CreateProposal(string cid, CreateCommandProposalCommand command)
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
        [HttpPatch("{cid}/proposals")]
        public async Task<IActionResult> ApproveProposal(string cid, ApproveCommandProposalCommand command)
        {
            if (cid != command.CommandId)
                return BadRequest();
            
            return await Handle(command);
        }
        
        [HttpPost("{cid}/invoices")]
        public async Task<IActionResult> GenerateInvoice(string cid, GenerateCommandInvoiceCommand command)
        {
            if (cid != command.CommandId)
                return BadRequest();
            
            return await Handle(command);
        }
    }
}