using System.Threading.Tasks;
using GoLocal.Client.Api.Controllers.Base;
using GoLocal.Client.Application.Queries.Shops.GetShop;
using GoLocal.Client.Application.Queries.Shops.GetShops;
using MediatR;
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
        [HttpGet("{sid:int}")]
        public async Task<IActionResult> Get(int sid)
            => await Handle(new GetShopQuery(sid));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Get(GetShopsQuery query)
            => await Handle(query);
    }
}