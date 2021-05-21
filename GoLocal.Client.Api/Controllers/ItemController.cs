using System.Threading.Tasks;
using GoLocal.Client.Api.Controllers.Base;
using GoLocal.Client.Application.Queries.Items.GetItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Client.Api.Controllers
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
        [HttpGet("{iid:int}")]
        public async Task<IActionResult> Get(int sid, int iid)
            => await Handle(new GetItemQuery(sid, iid));
    }
}