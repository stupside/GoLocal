using System.Threading.Tasks;
using Azure;
using GoLocal.Core.Client.Api.Controllers.Base;
using GoLocal.Core.Client.Application.Queries.Invoices.GetInvoice;
using GoLocal.Core.Client.Application.Queries.Invoices.GetInvoices;
using GoLocal.Core.Client.Application.Queries.Invoices.GetInvoices.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Core.Client.Api.Controllers
{
    [Authorize]
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
        [HttpGet("{iid:int}")]
        public async Task<IActionResult> Get(int iid)
            => await Handle(new GetInvoiceQuery(iid));
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Page<InvoiceDto>), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> Get(GetInvoicesQuery query)
            => await Handle(query);
    }
}