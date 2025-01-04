using MediatR;
using Microsoft.Extensions.Logging;

namespace Zahtevki.Application.Behaviours
{

    public class LoggingPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> _logger;

        public LoggingPipelineBehaviour(ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting request {RequestName}, {DateTimeUtc}", typeof(TRequest).Name, DateTime.UtcNow);

            try
            {
                var result = await next();
                _logger.LogInformation("Completed request {RequestName} at {DateTimeUtc}", typeof(TRequest).Name, DateTime.UtcNow);
                return result;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("Request {RequestName} failed at {DateTimeUtc} with error: {ErrorMessage}", typeof(TRequest).Name, DateTime.UtcNow, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Request {RequestName} failed at {DateTimeUtc}", typeof(TRequest).Name, DateTime.UtcNow);
                throw;
            }
        }
    }
}
