using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Shared.Bus.Results;
using GoLocal.Shared.Bus.Results.Factory;
using MediatR;

namespace GoLocal.Shared.Bus.Commons.Mediator
{
    public abstract class AbstractRequestHandler<TRequest> : IRequestHandler<TRequest, Result>
        where TRequest : AbstractRequest
    {
        public abstract Task<Result> Handle(TRequest request, CancellationToken cancellationToken);

        protected Result Ok()
            => ResultFactory.Ok();

        protected Result NotFound<TEntity>(object id = null)
            => ResultFactory.NotFound<TEntity>(id);

        protected Result BadRequest(string message, List<object> errors = null)
            => ResultFactory.BadRequest(message, errors);
        
        protected Result Unauthorized()
            => ResultFactory.Unauthorized();
    }
    
    public abstract class AbstractRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse>> 
        where TRequest : AbstractRequest<TResponse>
    {
        public abstract Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);

        protected Result<TResponse> Ok(TResponse entity)
            => ResultFactory.Ok(entity);

        protected Result<TResponse> BadRequest(string message, List<object> errors = null)
            => ResultFactory.BadRequest<TResponse>(message, errors);

        protected Result<TResponse> Unauthorized()
            => ResultFactory.Unauthorized<TResponse>();
        
        protected Result<TResponse> NotFound<TEntity>(object id = null)
            => ResultFactory.NotFound<TEntity>(id).ToResult<TResponse>();
    }
}