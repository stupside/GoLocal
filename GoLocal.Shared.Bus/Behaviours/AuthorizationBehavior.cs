using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace GoLocal.Shared.Bus.Behaviours
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            Type type = request.GetType();

            return next();
        }
    }
}