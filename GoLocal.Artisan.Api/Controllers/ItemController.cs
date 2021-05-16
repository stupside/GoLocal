using System;
using System.Threading.Tasks;
using GoLocal.Artisan.Api.Controllers.Base;
using GoLocal.Artisan.Application.Commands.Items.CreateItem;
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
        
        [HttpGet]
        public async Task<IActionResult> Get(int sid)
            => throw new NotImplementedException();

        [HttpPut("products")]
        public async Task<IActionResult> CreateProduct(int sid, CreateItemCommand command)
        {
            command.SetItemType<Product>();
            return await Handle(command);

        }

        [HttpPut("services")]
        public async Task<IActionResult> CreateService(int sid, CreateItemCommand command)
        {
            command.SetItemType<Service>();
            return await Handle(command);
        }
        
        [HttpPost]
        public async Task<IActionResult> Update(int sid)
            => throw new NotImplementedException();
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int sid)
            => throw new NotImplementedException();
    }
}