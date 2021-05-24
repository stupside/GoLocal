using System.Threading.Tasks;
using GoLocal.Client.Api.Controllers.Base;
using GoLocal.Client.Application.Queries.Shops.GetShop;
using GoLocal.Client.Application.Queries.Shops.GetShops;
using GoLocal.Client.Application.Queries.Shops.GetShops.Models;
using GoLocal.Shared.Bus.Results.Pages;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Client.Api.Controllers
{
    [Route("api/shops")]
    public class ShopController : ApiController
    {
        public ShopController(IMediator mediator) : base(mediator)
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(GetShopResponse), StatusCodes.Status200OK)]
        [HttpGet("{sid:int}")]
        public async Task<IActionResult> Get(int sid)
            => await Handle(new GetShopQuery(sid));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Page<ShopDto>), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> Get(GetShopsQuery query)
            => await Handle(query);
    }
}