using Microsoft.AspNetCore.Mvc.Filters;

namespace HomeWork_09._02._2026.Filters
{
    public class ContentSizeFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var originalBody = context.HttpContext.Response.Body;

            using var buffer = new MemoryStream();
            context.HttpContext.Response.Body = buffer;

            var responseContext = await next();

            buffer.Position = 0;
            var sizeInBytes = buffer.Length;

            if (sizeInBytes > 200)
            {
                context.HttpContext.Response.Body = originalBody;
                context.HttpContext.Response.StatusCode = 413;
                await context.HttpContext.Response.WriteAsync("Response too large");
                return;
            }

            buffer.Position = 0;
            await buffer.CopyToAsync(originalBody);
            context.HttpContext.Response.Body = originalBody;
        }
    }
}