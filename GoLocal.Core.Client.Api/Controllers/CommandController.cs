using System.Threading.Tasks;
using GoLocal.Bus.Results.Pages;
using GoLocal.Core.Client.Api.Controllers.Base;
using GoLocal.Core.Client.Application.Commands.Commands.ApproveCommandProposal;
using GoLocal.Core.Client.Application.Commands.Commands.CreateCommand;
using GoLocal.Core.Client.Application.Commands.Commands.CreateCommandProposal;
using GoLocal.Core.Client.Application.Queries.Commands.GetCommands;
using GoLocal.Core.Client.Application.Queries.Commands.GetCommands.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Core.Client.Api.Controllers
{
    [Authorize]
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
        [ProducesResponseType(typeof(Page<CommandDto>), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> Get(GetCommandsQuery query)
            => await Handle(query);

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