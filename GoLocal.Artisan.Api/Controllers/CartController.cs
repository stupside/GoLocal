using System;
using System.Threading.Tasks;
using GoLocal.Artisan.Api.Controllers.Base;
using GoLocal.Artisan.Application.Commands.Carts.AddCartPackage;
using GoLocal.Artisan.Application.Commands.Carts.RemoveCartPackage;
using GoLocal.Artisan.Application.Queries.Shops.GetShop;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Artisan.Api.Controllers
{
    [Authorize]
    [Route("api/shops/{sid:int}/carts")]
    public class CartController : ApiController
    {
        public CartController(IMediator mediator) : base(mediator)
        {
        }
        
        [HttpPut("package")]
        public async Task<IActionResult> Add(int sid, AddCartPackageCommand command)
        {
            if (sid != command.ShopId)
                return BadRequest();
            return await Handle(command);
        }
        
        [HttpDelete("package")]
        public async Task<IActionResult> Remove(int sid, RemoveCartPackageCommand command)
        {
            throw new NotImplementedException();
        }
    }
}