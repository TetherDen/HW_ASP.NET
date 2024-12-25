using HW_14.Data;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HW_14.Filters
{
    public class CurrencyFilterAttribute : Attribute, IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var db = context.HttpContext.RequestServices.GetRequiredService<ApplicationContext>();

            string currency = db.Currencies.FirstOrDefault()?.Name ?? "USD";
            context.HttpContext.Items["currency"] = currency;

            await next();

        }
    }
}
