using GoLocal.Shared.Bus.Results;
using MediatR;

namespace GoLocal.Shared.Bus.Commons.Mediator
{
    public abstract class AbstractRequest<TResponse> : IRequest<Result<TResponse>>
    {
    }
    
    public abstract class AbstractRequest : AbstractRequest<Unit>
    {
    }
}