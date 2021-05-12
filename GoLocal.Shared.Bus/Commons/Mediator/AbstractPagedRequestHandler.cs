using System.Collections.Generic;
using GoLocal.Shared.Bus.Commons.Mediator.Abstraction;
using GoLocal.Shared.Bus.Results;
using GoLocal.Shared.Bus.Results.Pages;

namespace GoLocal.Shared.Bus.Commons.Mediator
{
    public abstract class AbstractPagedRequestHandler<TRequest, TResponse> : AbstractContainedRequestHandler<TRequest, Page<TResponse>> 
        where TRequest : AbstractRequest<Page<TResponse>>
    {
        protected Result<Page<TResponse>> Ok(IEnumerable<TResponse> list, int count) 
            => Ok(new Page<TResponse>(list, count));
    }
}