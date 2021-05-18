using System.Threading.Tasks;
using GoLocal.Artisan.Api.Controllers.Base;
using GoLocal.Artisan.Application.Commands.Packages.CreatePackage;
using GoLocal.Artisan.Application.Commands.Packages.UpdatePackageStocks;
using GoLocal.Artisan.Application.Commands.Shops.CreateShop;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Artisan.Api.Controllers
{
    [Route("api/shops/{sid:int}/items/{iid:int}/packages")]
    public class PackageController : ApiController
    {
        public PackageController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="iid"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Create(int sid, int iid, CreatePackageCommand command)
        {
            if (sid != command.ShopId)
                return BadRequest();

            if (iid == command.ItemId)
                return BadRequest();

            return await Handle(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="iid"></param>
        /// <param name="pid"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch("{pid:int}/stocks")]
        public async Task<IActionResult> UpdateStocks(int sid, int iid, int pid, UpdatePackageStocksCommand command)
        {
            if (sid != command.ShopId)
                return BadRequest();

            if (iid == command.ItemId)
                return BadRequest();
            
            if (pid == command.PackageId)
                return BadRequest();
            
            return await Handle(command);
        }
    }
}