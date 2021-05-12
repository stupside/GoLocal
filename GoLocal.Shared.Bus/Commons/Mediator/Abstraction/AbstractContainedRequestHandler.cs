using System.Threading;
using System.Threading.Tasks;
using GoLocal.Shared.Bus.Results;
using GoLocal.Shared.Bus.Results.Enums;
using MediatR;

namespace GoLocal.Shared.Bus.Commons.Mediator.Abstraction
{
    public abstract class AbstractContainedRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse>> 
        where TRequest : AbstractRequest<TResponse>
    {
        public abstract Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
        
        protected Result<TResponse> Ok(TResponse data = default) 
            => new Result<TResponse>(data);

        protected Result<TResponse> NotFound<TEntity>()
            => NotFound($"{typeof(TEntity).Name} not found.");
        
        protected Result<TResponse> NotFound<TEntity>(object id)
            => NotFound($"{typeof(TEntity).Name} with id '{id}' not found.");
        
        protected Result<TResponse> BadRequest(string message)
            => new Result<TResponse>(ResultType.BadRequest, message);
        
        private Result<TResponse> NotFound(string message)
            => new Result<TResponse>(ResultType.NotFound, message);
    }
}