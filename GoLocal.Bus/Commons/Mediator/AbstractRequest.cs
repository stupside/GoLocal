using GoLocal.Bus.Results;
using MediatR;

namespace GoLocal.Bus.Commons.Mediator
{
    public abstract class AbstractRequest<TResponse> : IRequest<Result<TResponse>>
    {
    }
    
    public abstract class AbstractRequest : IRequest<Result>
    {
    }
}