using GoLocal.Core.Client.Api.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    }
}