using System.Threading.Tasks;
using GoLocal.Bus.Results.Pages;
using GoLocal.Core.Artisan.Api.Controllers.Base;
using GoLocal.Core.Artisan.Application.Commands.Invoices.MakeInvoiceReady;
using GoLocal.Core.Artisan.Application.Queries.Invoices.GetInvoice;
using GoLocal.Core.Artisan.Application.Queries.Invoices.GetInvoices;
using GoLocal.Core.Artisan.Application.Queries.Invoices.GetInvoices.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Core.Artisan.Api.Controllers
{
    [Route("api/shops/{sid:int}/invoices")]
    public class InvoiceController : ApiController
    {
        public InvoiceController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="iid"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(GetInvoiceResponse), StatusCodes.Status200OK)]
        [HttpGet("{iid:int}")]
        public async Task<IActionResult> Get(int sid, int iid)
            => await Handle(new GetInvoiceQuery(sid, iid));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Page<InvoiceDto>), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> Get(int sid, GetInvoicesQuery query)
        {
            if (sid != query.ShopId)
                return BadRequest();
            
            return await Handle(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="iid"></param>
        /// <returns></returns>
        [HttpPatch("{iid:int}/ready")]
        public async Task<IActionResult> MakeReady(int sid, int iid)
            => await Handle(new MakeInvoiceReadyCommand(sid, iid));
    }
}