using System.Threading.Tasks;
using GoLocal.Core.Client.Api.Controllers.Base;
using GoLocal.Core.Client.Application.Queries.Items.GetItem;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Core.Client.Api.Controllers
{
    [Route("api/shops/{sid:int}/items")]
    public class ItemController : ApiController
    {
        public ItemController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="iid"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(GetItemResponse), StatusCodes.Status200OK)]
        [HttpGet("{iid:int}")]
        public async Task<IActionResult> Get(int sid, int iid)
            => await Handle(new GetItemQuery(sid, iid));
    }
}