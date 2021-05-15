using System;
using System.Threading.Tasks;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using GoLocal.Shared.Bus.Results.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoLocal.Artisan.Api.Controllers.Base
{
    //[Authorize]
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        protected ApiController(IMediator mediator)
            => _mediator = mediator;

        protected async Task<IActionResult> Handle<TResponse>(AbstractRequest<TResponse> request)
        {
            Result<TResponse> response;
            
            try
            {
                response = await _mediator.Send(request);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return response.Type switch
            {
                ResultType.Ok => Ok(response.Entity),
                ResultType.Unauthorized => Unauthorized(response.Message),
                ResultType.BadRequest => BadRequest(response.Message),
                ResultType.NotFound => NotFound(response.Message),
                _ => NotFound()
            };
        }
    }
}