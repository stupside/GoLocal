using System.Threading.Tasks;
using GoLocal.Client.Api.Controllers.Base;
using GoLocal.Client.Application.Commands.Carts.AddCartPackage;
using GoLocal.Client.Application.Commands.Carts.RemoveCartPackage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Client.Api.Controllers
{
    [Route("api/shops/{sid:int}/carts")]
    public class CartController : ApiController
    {
        public CartController(IMediator mediator) : base(mediator)
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("packages")]
        public async Task<IActionResult> Add(int sid, AddCartPackageCommand command)
        {
            if (sid != command.ShopId)
                return BadRequest();
            
            return await Handle(command);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("packages")]
        public async Task<IActionResult> Remove(int sid, RemoveCartPackageCommand command)
        {
            if (sid != command.ShopId)
                return BadRequest();
            
            return await Handle(command);
        }
    }
}