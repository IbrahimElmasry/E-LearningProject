using E_LearningProject.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace E_LearningProject.MiddleWares
{
    public class SessionLoggingMiddlewareOptions
    {
        public bool LogBeforeRequest { get; set; } = true;
        public bool LogAfterRequest { get; set; } = false;
        public bool LogSessionValues { get; set; } = false;
    }
    public class SessionLoggingMiddleware
    {
        private readonly RequestDelegate _next; // Field to store the next middleware delegate
        private readonly ILogger<SessionLoggingMiddleware> _logger;
        private readonly SessionLoggingMiddlewareOptions _options;

        public SessionLoggingMiddleware(RequestDelegate next, IOptions<SessionLoggingMiddlewareOptions> options, ILogger<SessionLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context) // InvokeAsync only takes HttpContext
        {
            // Log session details before request processing (if enabled)
            if (_options.LogBeforeRequest && context.Session.IsAvailable)
            {
                LogSessionDetails("Before request", context);
            }

            // Call the next middleware in the pipeline
            await _next(context); // Pass the context to the next middleware

            // Log session details after request processing (if enabled)
            if (_options.LogAfterRequest && context.Session.IsAvailable)
            {
                LogSessionDetails("After request", context);
            }
        }

        private void LogSessionDetails(string messagePrefix, HttpContext context)
        {
            if (context.Session != null)
            {
                var sessionKeys = string.Join(", ", context.Session.Keys);
                _logger.LogInformation($"{messagePrefix}: Session ID: {context.Session.Id}, Keys: {sessionKeys}");

                if (_options.LogSessionValues)
                {
                    foreach (var key in context.Session.Keys)
                    {
                        _logger.LogInformation($"  {key}: {context.Session.GetString(key)}");
                    }
                }
            }
            else
            {
                _logger.LogWarning($"{messagePrefix}: Session is null");
            }
        }
    }
}
