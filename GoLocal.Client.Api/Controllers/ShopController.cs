using System.Threading.Tasks;
using GoLocal.Client.Api.Controllers.Base;
using GoLocal.Client.Application.Queries.GetShop;
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
        
        [HttpGet("{sid:int}")]
        public async Task<IActionResult> Get(int sid)
            => await Handle(new GetShopCommand(sid));
    }
}