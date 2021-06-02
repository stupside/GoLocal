using System.Threading.Tasks;
using GoLocal.Core.Artisan.Api.Controllers.Base;
using GoLocal.Core.Artisan.Application.Commands.Items.CreateItem;
using GoLocal.Core.Artisan.Application.Commands.Items.UpdateItem;
using GoLocal.Core.Artisan.Application.Commands.Items.UpdateItemDescription;
using GoLocal.Core.Artisan.Application.Commands.Items.UpdateItemImage;
using GoLocal.Core.Artisan.Application.Commands.Items.UpdateItemVisibility;
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
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> Update(int sid, UpdateItemCommand command)
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
        [HttpPatch("description")]
        public async Task<IActionResult> UpdateDescription(int sid, UpdateItemDescriptionCommand command)
        {
            if (sid != command.ShopId)
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
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch("visibility")]
        public async Task<IActionResult> UpdateVisibility(int sid, UpdateItemVisibilityCommand command)
        {
            if (sid != command.ShopId)
                return BadRequest();
            
            return await Handle(command);
        }
    }
}