using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using GoLocal.Shared.Bus.Results;
using GoLocal.Shared.Bus.Results.Enums;
using GoLocal.Shared.Bus.Results.Interfaces;
using MediatR;

namespace GoLocal.Shared.Bus.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TResponse : class, IResult
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!_validators.Any())
                return await next();
            
            var context = new ValidationContext<TRequest>(request);

            var results = await Task.WhenAll(_validators
                .Select(v => v.ValidateAsync(context, cancellationToken)));
            
            var failures = results
                .SelectMany(r => r.Errors)
                .Where(f => f != null).ToList();

            if (failures.Count != 0)
                return new Result<List<ValidationFailure>>(ResultType.BadRequest, failures) as TResponse;

            return await next();
        }
    }
}