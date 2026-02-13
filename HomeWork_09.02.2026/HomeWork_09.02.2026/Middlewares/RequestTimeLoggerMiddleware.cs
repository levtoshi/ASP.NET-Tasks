using System.Diagnostics;

namespace HomeWork_09._02._2026.Middlewares
{
    public class RequestTimeLoggerMiddleware : IMiddleware
    {
        private readonly ILogger<RequestTimeLoggerMiddleware> _logger;

        public RequestTimeLoggerMiddleware(ILogger<RequestTimeLoggerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await next.Invoke(context);

            stopwatch.Stop();

            _logger.LogInformation($"Endpoint: {context.Request.Path}\nTime: {stopwatch.ElapsedMilliseconds} ms\n");
        }
    }
}