using System.Threading.Tasks;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Identity.Api.Controllers.Commons
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<IActionResult> Send<TResponse>(AbstractRequest<TResponse> request)
        {
            var response = await _mediator.Send(request);

            switch (response.Type)
            {
                case ResultType.Ok:
                    return Ok(new { response.Entity, response.Message });
                case ResultType.Unauthorized:
                    return Unauthorized(response.Message);
                case ResultType.BadRequest:
                    return BadRequest(response.Message);
                case ResultType.NotFound:
                    return NotFound(response.Message);
                default:
                    return NoContent();
            }
        }
    }
}