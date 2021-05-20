using System.Threading.Tasks;
using GoLocal.Artisan.Api.Controllers.Base;
using GoLocal.Artisan.Application.Queries.Commands.GetCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Artisan.Api.Controllers
{
    [Route("api/shops/{sid:int}/commands")]
    public class CommandController : ApiController
    {
        public CommandController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Get(int sid, GetCommandsQuery query)
        {
            if (sid != query.ShopId)
                return BadRequest();
            return await Handle(query);
        }
    }
}