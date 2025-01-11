using Microsoft.AspNetCore.Mvc;

namespace HW_15_Email_FutureMe.Util
{
    public class HtmlResult : IActionResult
    {
        string htmlCode;
        public HtmlResult(string html)
        {
            htmlCode = html;
        }
        public async Task ExecuteResultAsync(ActionContext context)
        {
            string fullHtmlCode = "<!DOCTYPE html><html><head>";
            fullHtmlCode += "<title>Главная страница</title>";
            fullHtmlCode += "<meta charset=utf-8 />";
            fullHtmlCode += "</head> <link href='https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css' rel='stylesheet'> <body>";
            fullHtmlCode += htmlCode;
            fullHtmlCode += "</body></html>";
            await context.HttpContext.Response.WriteAsync(fullHtmlCode);
        }



    }
}
