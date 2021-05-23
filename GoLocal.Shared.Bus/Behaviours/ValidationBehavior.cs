using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using GoLocal.Shared.Bus.Results;
using GoLocal.Shared.Bus.Results.Enums;
using MediatR;

namespace GoLocal.Shared.Bus.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : AbstractResult
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
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
                .Where(f => f != null)
                .ToList();


            if (!failures.Any()) return await next();
            
            TResponse response = (TResponse)Activator.CreateInstance(typeof(TResponse));
            if (response == null)
                throw new NullReferenceException();

            response.Errors = new List<object>(failures.Select(m => new { m.ErrorMessage, m.PropertyName, m.AttemptedValue }));
            response.Status = ResultStatus.BadRequest;

            return response;
        }
    }
}