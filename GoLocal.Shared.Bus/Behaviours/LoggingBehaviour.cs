using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace GoLocal.Shared.Bus.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger<LoggingBehaviour<TRequest>> _logger;

        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest>> logger)
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