using System.Runtime.CompilerServices;

namespace Lesson_03.Middleware
{
    public static class TokenExtensions
    {
        public static IApplicationBuilder UseToken(this IApplicationBuilder builder,string token)
        {
            return builder.UseMiddleware<TokenMiddleware>(token);
        }
    }
}
