using System.Collections.Generic;
using GoLocal.Bus.Results;
using GoLocal.Bus.Results.Pages;

namespace GoLocal.Bus.Commons.Mediator
{
    public abstract class AbstractPagedRequestHandler<TRequest, TResponse> : AbstractRequestHandler<TRequest, Page<TResponse>> 
        where TRequest : AbstractRequest<Page<TResponse>>
    {
        protected Result<Page<TResponse>> Ok(List<TResponse> list, int count) 
            => Ok(new Page<TResponse>(list, count));
    }
}