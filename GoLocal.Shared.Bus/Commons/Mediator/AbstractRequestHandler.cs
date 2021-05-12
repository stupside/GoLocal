using GoLocal.Shared.Bus.Commons.Mediator.Abstraction;
using MediatR;

namespace GoLocal.Shared.Bus.Commons.Mediator
{
    public abstract class AbstractRequestHandler<TRequest> : AbstractRequestHandler<TRequest, Unit>
        where TRequest : AbstractRequest<Unit>
    {
    }
    
    public abstract class AbstractRequestHandler<TRequest, TResponse> : AbstractContainedRequestHandler<TRequest, TResponse> 
        where TRequest : AbstractRequest<TResponse>
    {
    }
}