using System.Threading.Tasks;
using GoLocal.Artisan.Api.Controllers.Base;
using GoLocal.Artisan.Application.Queries.Invoices.GetInvoice;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Artisan.Api.Controllers
{
    [Route("api/invoices")]
    public class InvoiceController : ApiController
    {
        public InvoiceController(IMediator mediator) : base(mediator)
        {
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(int iid)
            => await Handle(new GetInvoiceQuery(iid));
    }
}