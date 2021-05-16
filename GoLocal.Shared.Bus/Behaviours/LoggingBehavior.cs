using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace GoLocal.Shared.Bus.Behaviours
{
    public class LoggingBehavior<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger<LoggingBehavior<TRequest>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest>> logger)
        {
            _logger = logger;
        }


        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{Request} Started", typeof(TRequest).Name);
            
            return Task.CompletedTask;
        }
    }
}