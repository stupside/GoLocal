using System;
using System.Threading.Tasks;
using GoLocal.Artisan.Api.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Artisan.Api.Controllers
{
    [Route("api/shops/{sid:int}")]
    public class ItemController : ApiController
    {
        public ItemController(IMediator mediator) : base(mediator)
        {
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(int sid)
            => throw new NotImplementedException();

        [HttpPut("products")]
        public async Task<IActionResult> CreateProduct(int sid)
            => throw new NotImplementedException();
        
        [HttpPut("services")]
        public async Task<IActionResult> CreateService(int sid)
            => throw new NotImplementedException();
        
        [HttpPost]
        public async Task<IActionResult> Update(int sid)
            => throw new NotImplementedException();
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int sid)
            => throw new NotImplementedException();
    }
}