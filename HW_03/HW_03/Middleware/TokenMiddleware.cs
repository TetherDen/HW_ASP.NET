namespace Lesson_03.Middleware
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate next;
        private string token;

        public TokenMiddleware(RequestDelegate next, string token)
        {
            this.next = next;
            this.token = token;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Query["token"];
            if(token != this.token && context.Request.Path == "/getbooks")
            //if (context.Request.Path == "/getbooks" && token != this.token)
            {

                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Not authorized");
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }
}
