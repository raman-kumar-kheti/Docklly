using Microsoft.AspNetCore;

namespace Docklly.Middleware
{
    public class HttpCustomMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public void GetHttpContext(HttpContext context)
        {
            var getHeader = context.Request.Headers;
            Console.WriteLine(getHeader);
            _next(context);
        }
    }

}