using System.Threading.Tasks;
using GoLocal.Artisan.Api.Controllers.Base;
using GoLocal.Artisan.Application.Commands.Shops.CreateShop;
using GoLocal.Artisan.Application.Commands.Shops.DeleteShop;
using GoLocal.Artisan.Application.Commands.Shops.UpdateShop;
using GoLocal.Artisan.Application.Commands.Shops.UpdateShopContact;
using GoLocal.Artisan.Application.Commands.Shops.UpdateShopLocalisation;
using GoLocal.Artisan.Application.Commands.Shops.UpdateShopOpening;
using GoLocal.Artisan.Application.Queries.Shops.GetShop;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Artisan.Api.Controllers
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

        /// <summary>
        /// Create a shop
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
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
        /// Update localisation information for the desired shop
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch("localisation")]
        public async Task<IActionResult> UpdateLocalisation(UpdateShopLocalisationCommand command)
            => await Handle(command);
        
        /// <summary>
        /// Update an opening for the desired shop
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch("opening")]
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