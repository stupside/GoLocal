using System;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Shared.Bus.Results.Enums;
using MediatR;

namespace GoLocal.Shared.Bus2
{

    public interface IResult
    {
        ResultStatus Status { get; }
        string Description { get; }
        object Object { get; }
    }
    
    public interface IResult<out T> : IResult
    {
        T Data { get; }
        Type Type { get; }
    }

    public abstract class AbstractResult<T> : IResult<T>
    {
        public virtual ResultStatus Status { get; private set; }
        public virtual string Description { get; private set; }
        public virtual T Data => (T)Object;
        public Type Type => typeof(T);
        public object Object { get; private set; }

        public AbstractResult<T> WithDescription(string message)
        {
            Description = message;
            return this;
        }
        
        public AbstractResult<T> WithStatus(ResultStatus status)
        {
            Status = status;
            return this;
        }
        
        public AbstractResult<T> WithData(T data)
        {
            Object = data;
            return this;
        }
    }

    public class Result<T> : AbstractResult<T>
    {
    }

    public abstract class ResultBuilder<TResponse>
    {
        protected Result<TResponse> Ok(TResponse entity = default)
            => new Result<TResponse>().WithData(entity).WithStatus(ResultStatus.Ok) as Result<TResponse>;
        
        protected Result<TResponse> BadRequest(string message = default)
            => new Result<TResponse>()
                .WithDescription(message)
                .WithStatus(ResultStatus.BadRequest) as Result<TResponse>;
        
        protected Result<TResponse> NotFound<T>()
            => new Result<TResponse>()
                .WithDescription($"Entity of type {nameof(T)} not found")
                .WithStatus(ResultStatus.NotFound) as Result<TResponse>;
        
        protected Result<TResponse> NotFound<T>(object id)
            => new Result<TResponse>()
                .WithDescription($"Entity of type {nameof(T)} with id '{id}' not found")
                .WithStatus(ResultStatus.NotFound) as Result<TResponse>;
        
        protected Result<TResponse> Unauthorized()
            => new Result<TResponse>().WithStatus(ResultStatus.Unauthorized) as Result<TResponse>;
    }

    public abstract class AbstractRequestHandler<TRequest> : AbstractRequestHandler<TRequest, Unit>
        where TRequest : IRequest<Result<Unit>>
    {
    }
    
    public abstract class AbstractRequestHandler<TRequest, TResponse> : ResultBuilder<TResponse>, IRequestHandler<TRequest, Result<TResponse>>
        where TRequest : IRequest<Result<TResponse>>
    {
        public abstract Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
    }

    public abstract class AbstractRequestPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<Result<TResponse>>
    {
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next);
    }
}