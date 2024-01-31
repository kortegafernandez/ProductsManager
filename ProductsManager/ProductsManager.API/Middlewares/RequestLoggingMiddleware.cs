using System.Diagnostics;

namespace ProductsManager.API.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;       

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            // Let the request proceed through the pipeline
            await _next(context);
            
            stopwatch.Stop();           
            var route = context.Request.Path.Value;

            if (context.Response.StatusCode == 200)
            {                
                var logData = $"Route: {route}, Time: {stopwatch.ElapsedMilliseconds}ms";                
                _logger.LogInformation(logData);
            }
        }
    }
}
