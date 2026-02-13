using System.Text;

namespace HomeWork_09._02._2026.Middlewares
{
    public class CopyrightMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var originalBody = context.Response.Body;

            using var tempStream = new MemoryStream();
            context.Response.Body = tempStream;

            await next.Invoke(context);

            tempStream.Position = 0;
            var bodyText = await new StreamReader(tempStream).ReadToEndAsync();

            bodyText += "\n\n Copyright by Satoshi";

            var bytes = Encoding.UTF8.GetBytes(bodyText);

            context.Response.Body = originalBody;
            context.Response.ContentLength = bytes.Length;

            await context.Response.Body.WriteAsync(bytes);
        }
    }
}