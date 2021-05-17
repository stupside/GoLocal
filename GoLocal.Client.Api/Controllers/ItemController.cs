using System;
using System.Threading.Tasks;
using GoLocal.Client.Api.Controllers.Base;
using GoLocal.Client.Application.Queries.Items.GetItem;
using GoLocal.Client.Application.Queries.Items.GetItems;
using GoLocal.Domain.Entities;
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

        [HttpGet("{iid:int}")]
        public async Task<IActionResult> Get(int sid, int iid)
            => await Handle(new GetItemQuery(sid, iid));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("paged")]
        public async Task<IActionResult> Get(int sid, GetItemsQuery query)
        {
            if (sid != query.ShopId)
                return BadRequest();

            return await Handle(query);
        }
    }
}