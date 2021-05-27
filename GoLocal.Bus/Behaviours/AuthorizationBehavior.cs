using System;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Authorizers;
using GoLocal.Bus.Results;
using GoLocal.Bus.Results.Enums;
using MediatR;

namespace GoLocal.Bus.Behaviours
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : AbstractResult
    {
        private readonly IAuthorizerHandler _authorizer;

        public AuthorizationBehavior(IAuthorizerHandler authorizer)
        {
            _authorizer = authorizer;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var authorized = await _authorizer.Handle(request);
            
            if (authorized.Succeed) return await next();
            
            TResponse response = (TResponse)Activator.CreateInstance(typeof(TResponse));
            if (response == null)
                throw new NullReferenceException();

            if (authorized.NotFound)
            {
                response.Status = ResultStatus.NotFound;
            }
            else
            {
                response.Status = ResultStatus.Unauthorized;
            }
            return response;
        }
    }
}