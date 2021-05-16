using System;
using System.Threading.Tasks;
using GoLocal.Shared.Bus.Results;
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

        protected async Task<IActionResult> Handle<TResponse>(IRequest<TResponse> request)
            where TResponse : AbstractResult
        {
            TResponse response;
            try
            {
                response = await _mediator.Send(request);
            }
            catch (Exception)
            {
                throw;
            }

            return response.Status switch
            {
                ResultStatus.Ok => Ok(response.Entity),
                ResultStatus.Unauthorized => Unauthorized(response.Message),
                ResultStatus.BadRequest => BadRequest(new { response.Errors, response.Message }),
                ResultStatus.NotFound => NotFound(response.Message),
                _ => NotFound()
            };
        }
    }
}