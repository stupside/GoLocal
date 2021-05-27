using System.Threading.Tasks;
using GoLocal.Bus.Results.Pages;
using GoLocal.Core.Artisan.Api.Controllers.Base;
using GoLocal.Core.Artisan.Application.Commands.Shops.CreateShop;
using GoLocal.Core.Artisan.Application.Commands.Shops.DeleteShop;
using GoLocal.Core.Artisan.Application.Commands.Shops.UpdateShop;
using GoLocal.Core.Artisan.Application.Commands.Shops.UpdateShopContact;
using GoLocal.Core.Artisan.Application.Commands.Shops.UpdateShopLocation;
using GoLocal.Core.Artisan.Application.Commands.Shops.UpdateShopOpening;
using GoLocal.Core.Artisan.Application.Queries.Shops.GetShops;
using GoLocal.Core.Artisan.Application.Queries.Shops.GetShops.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Core.Artisan.Api.Controllers
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
        /// <param name="query"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Page<ShopDto>), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> Get(GetShopsQuery query)
            => await Handle(query);

        /// <summary>
        /// Create a shop
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [HttpPut]
        public async Task<IActionResult> Create(CreateShopCommand command)
            => await Handle(command);
        
        /// <summary>
        /// Update basic information for the desired shop
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> Update(UpdateShopCommand command)
            => await Handle(command);
        
        /// <summary>
        /// Update contact information for the desired shop
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch("contact")]
        public async Task<IActionResult> UpdateContact(UpdateShopContactCommand command)
            => await Handle(command);
        
        /// <summary>
        /// Update the location of the desired shop
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch("location")]
        public async Task<IActionResult> UpdateShopLocation(UpdateShopLocationCommand command)
            => await Handle(command);
        
        /// <summary>
        /// Update an opening for the desired shop
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch("openings")]
        public async Task<IActionResult> UpdateOpening(UpdateShopOpeningCommand command)
            => await Handle(command);
        
        /// <summary>
        /// Delete the desired shop
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteShopCommand command)
            => await Handle(command);
    }
}