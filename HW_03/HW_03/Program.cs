using Lesson_03.Middleware;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


//ѕредставим, что у вас есть минимальное API в ASP.NET Core, которое позвол€ет пользовател€м получать список книг из базы данных, при обращении по адресу У/allbooksФ.†
//ќднако, вы хотите добавить функциональность, котора€ позволит вам фильтровать список книг по категори€м только если пользователь авторизован, по адресу У/getbooks?token=token12345&category=musicФ.
//¬ данном случае, пользователь будет считатьс€ авторизованным, если у него в запросе присутствует token, с определенным значением.
//¬ы должны использовать собственный класс Middleware.
// ниги возвращать в виде HTML таблицы.

List<Book> books = new List<Book>()
{
    new Book("book1", "category1"),
    new Book("book2", "category2"),
    new Book("book3", "category2"),
    new Book("book4", "category1"),
    new Book("book5", "category2"),
};

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/books" && context.Request.Method == "GET")
    {
        Console.WriteLine($"Request: {context.Request.Method}  {context.Request.Path} {DateTime.Now}");
        await context.Response.WriteAsync(GenerateHtmlPage(BuildHtmlTable(books), "All books"));
    }
    else
    {
        await next.Invoke();
    }
});

//localhost:7123/getbooks?token=12345&category=category2

app.UseWhen(context => context.Request.Path == "/getbooks" && context.Request.Method == "GET",
    appBuilder =>
    {
        appBuilder.UseToken("12345");

        appBuilder.Run(async (context) =>
        {
            Console.WriteLine($"Request: {context.Request.Method}  {context.Request.Path} Token: {context.Request.Query["token"]}, Date: {DateTime.Now}");
            string category = context.Request.Query["category"];
            List<Book> booksByCategory = books.Where(book => book.Category.ToLower() == category.ToLower()).ToList();
            await context.Response.WriteAsync(GenerateHtmlPage(BuildHtmlTable(booksByCategory), $"Books by {booksByCategory} category."));

        });
    });

app.Run();


static string BuildHtmlTable<T>(IEnumerable<T> collection)
{
    StringBuilder tableHtml = new StringBuilder();
    tableHtml.Append("<table class=\"table\">");
    PropertyInfo[] properties = typeof(T).GetProperties();

    tableHtml.Append("<tr>");
    foreach (PropertyInfo property in properties)
    {
        tableHtml.Append($"<th>{property.Name}</th>");
    }
    tableHtml.Append("<th>Actions</th>");
    tableHtml.Append("</tr>");
    foreach (T item in collection)
    {
        tableHtml.Append("<tr>");
        foreach (PropertyInfo property in properties)
        {
            object value = property.GetValue(item);
            tableHtml.Append($"<td>{value}</td>");
        }

        tableHtml.Append("</tr>");
    }
    tableHtml.Append("</table>");
    return tableHtml.ToString();
}

static string GenerateHtmlPage(string body, string header)
{
    string html = $"""
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset="utf-8" />
            <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet" 
            integrity="sha384-KK94CHFLLe+nY2dmCWGMq91rCGa5gtU4mk92HdvYe+M/SXH301p5ILy+dN9+nJOZ" crossorigin="anonymous">
            <title>{header}</title>
        </head>
        <body>
        <div class="container">
        <h2 class="d-flex justify-content-center">{header}</h2>
        <div class="mt-5">

        <a href="/add" class="btn btn-primary">Add User</a>

        </div>
        {body}
            <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/js/bootstrap.bundle.min.js" 
            integrity="sha384-ENjdO4Dr2bkBIFxQpeoTz1HIcje39Wm4jDKdf19U8gI4ddQ3GYNS7NTKfAdVQSZe" crossorigin="anonymous"></script>
        </div>
        </body>
        </html>
        """;
    return html;
}

public record Book(int Id, string Title, string Category)
{
    public Book(string Title, string Category) : this(0, Title, Category) { }
}