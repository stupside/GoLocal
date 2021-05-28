using System.Threading.Tasks;
using GoLocal.Core.Artisan.Api.Controllers.Base;
using GoLocal.Core.Artisan.Application.Commands.Items.CreateItem;
using GoLocal.Core.Artisan.Application.Commands.Items.DeleteItem;
using GoLocal.Core.Artisan.Application.Commands.Items.UpdateItem;
using GoLocal.Core.Artisan.Application.Commands.Items.UpdateItemImage;
using GoLocal.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Core.Artisan.Api.Controllers
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
        /// <param name="command"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [HttpPut("products")]
        public async Task<IActionResult> CreateProduct(int sid, CreateItemCommand command)
        {
            if (sid != command.ShopId)
                return BadRequest();
            
            command.SetItemType<Product>();
            return await Handle(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [HttpPut("services")]
        public async Task<IActionResult> CreateService(int sid, CreateItemCommand command)
        {
            if (sid != command.ShopId)
                return BadRequest();
            
            command.SetItemType<Service>();
            return await Handle(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="iid"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("{iid:int}")]
        public async Task<IActionResult> Update(int sid, int iid, UpdateItemCommand command)
        {
            if (sid != command.ShopId)
                return BadRequest();

            if (iid != command.ItemId)
                return BadRequest();
            
            return await Handle(command);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="iid"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPatch("{iid:int}/image")]
        public async Task<IActionResult> UpdateImage(int sid, int iid, IFormFile file)
            => await Handle(new UpdateItemImageCommand(sid, iid, file));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="iid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpDelete("{iid:int}/{name}")]
        public async Task<IActionResult> Delete(int sid, int iid, string name)
            => await Handle(new DeleteItemCommand(sid, iid, name));
    }
}