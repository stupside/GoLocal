using System.Threading.Tasks;
using GoLocal.Client.Api.Controllers.Base;
using GoLocal.Client.Application.Commands.Commands.ApproveCommandProposal;
using GoLocal.Client.Application.Commands.Commands.CreateCommand;
using GoLocal.Client.Application.Commands.Commands.CreateCommandProposal;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Client.Api.Controllers
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
        /// <param name="command"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpPut]
        public async Task<IActionResult> Create(CreateCommandCommand command)
            => await Handle(command);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
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
    }
}