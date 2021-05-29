using System.Threading.Tasks;
using GoLocal.Bus.Results.Pages;
using GoLocal.Core.Client.Api.Controllers.Base;
using GoLocal.Core.Client.Application.Commands.Carts.AddCartPackage;
using GoLocal.Core.Client.Application.Commands.Carts.DeleteCart;
using GoLocal.Core.Client.Application.Commands.Carts.GenerateCartInvoice;
using GoLocal.Core.Client.Application.Commands.Carts.RemoveCartPackage;
using GoLocal.Core.Client.Application.Queries.Carts.GetCarts;
using GoLocal.Core.Client.Application.Queries.Carts.GetCarts.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Core.Client.Api.Controllers
{
    [Authorize]
    [Route("api/carts")]
    public class CartController : ApiController
    {
        public CartController(IMediator mediator) : base(mediator)
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Page<CartDto>), StatusCodes.Status200OK)]
        [HttpPost("invoices")]
        public async Task<IActionResult> Get(GetCartsQuery query)
            => await Handle(query);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [HttpPost("shops/{sid:int}")]
        public async Task<IActionResult> GenerateInvoice(int sid, GenerateCartInvoiceCommand command)
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
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [HttpPut("shops/{sid:int}")]
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
        [HttpPost("shops/{sid:int}")]
        public async Task<IActionResult> Remove(int sid, RemoveCartPackageCommand command)
        {
            if (sid != command.ShopId)
                return BadRequest();
            
            return await Handle(command);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        [HttpDelete("shops/{sid:int}")]
        public async Task<IActionResult> Delete(int sid)
            => await Handle(new DeleteCartCommand(sid));
    }
}