using System;
using System.Threading.Tasks;
using GoLocal.Artisan.Api.Controllers.Base;
using GoLocal.Artisan.Application.Commands.Items.CreateItem;
using GoLocal.Artisan.Application.Commands.Items.DeleteItem;
using GoLocal.Artisan.Application.Commands.Items.UpdateItem;
using GoLocal.Artisan.Application.Queries.Items.GetItem;
using GoLocal.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Artisan.Api.Controllers
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
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="command"></param>
        /// <returns></returns>
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
        /// <param name="command"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpDelete("{iid:int}")]
        public async Task<IActionResult> Delete(int sid, int iid, DeleteItemCommand command)
        {
            if (sid != command.ShopId)
                return BadRequest();

            if (iid != command.ItemId)
                return BadRequest();

            return await Handle(command);
        }
    }
}