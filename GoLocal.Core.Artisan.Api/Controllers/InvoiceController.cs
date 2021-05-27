using System.Threading.Tasks;
using GoLocal.Core.Artisan.Api.Controllers.Base;
using GoLocal.Core.Artisan.Application.Queries.Invoices.GetInvoice;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Core.Artisan.Api.Controllers
{
    [Route("api/invoices")]
    public class InvoiceController : ApiController
    {
        public InvoiceController(IMediator mediator) : base(mediator)
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iid"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(GetInvoiceResponse), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> Get(int iid)
            => await Handle(new GetInvoiceQuery(iid));
    }
}