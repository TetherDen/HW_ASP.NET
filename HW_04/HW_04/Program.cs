using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


//Создать класс «User». Определить интерфейс и репозиторий по управлению пользователями, с доступными действиями:
//добавить, удалить, получить конкретного пользователя, редактировать, вывести всех пользователей. 
//Создать веб-сайт по управлению этими пользователями на несколько страниц (без базы данных, использовать подходящий жизненный цикл сервиса для полноценной работы в одном сеансе).
//Весь код можно писать в классе Program.cs или использовать отдельные представления. Обработать возможные ошибочные ситуации, к примеру передачу неверного Id (как в формате так и в плане существования).

builder.Services.AddSingleton<IUserRepository, UserRepository>();

var app = builder.Build();


app.MapGet("/", async context =>
{
    var repo = context.RequestServices.GetRequiredService<IUserRepository>();
    var users = repo.GetAllUsers();

    var html = HtmlGenerator.GenerateUsersPage(users);

    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.WriteAsync(html);
});


app.MapGet("/addUser", async context =>
{
    var htmlForm = HtmlGenerator.GenerateUserForm("/addUser", "Add");

    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.WriteAsync(htmlForm);
});


app.MapPost("/addUser", async context =>
{
    string name = context.Request.Form["name"];
    string email = context.Request.Form["email"];

    var newUser = new User { Name = name, Email = email };
    var repos = context.RequestServices.GetRequiredService<IUserRepository>();
    repos.AddUser(newUser);
    context.Response.Redirect("/");

});

app.MapGet("/editUser/{id}", async context =>
{
    //string strId = context.Request.Query["id"];
    string strId = context.Request.RouteValues["id"]?.ToString();
    if (string.IsNullOrEmpty(strId) || !int.TryParse(strId, out int id))
    {
        context.Response.StatusCode = 400;
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync($"<h3> Invalid User ID: \"{strId}\" <h3>");
    }
    else
    {
        var repos = context.RequestServices.GetRequiredService<IUserRepository>();
        User? user = repos.GetByIdUser(id);
        if(user == null)
        {
            context.Response.StatusCode = 404;
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync($"<h3>User with ID \"{id}\" not found</h3>");
        }

        var htmlForm = HtmlGenerator.GenerateUserForm("/editUser", "Edit", user);
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync(htmlForm);
    }  
});


app.MapPost("/editUser", async context =>
{
    string id = context.Request.Form["id"];
    string name = context.Request.Form["name"];
    string email = context.Request.Form["email"];

    if (!int.TryParse(id, out var userId))
    {
        context.Response.StatusCode = 400;
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync($"<h4>Invalid User ID: \"{id}\" <h4>");
    }

    var repo = context.RequestServices.GetRequiredService<IUserRepository>();
    var user = new User { Id = userId, Name = name, Email = email };
    repo.EditUser(user);

    context.Response.Redirect("/");
});


https://localhost:7260/deleteUser/1
app.MapGet("/deleteUser/{id}", async context =>
{
    string strId = context.Request.RouteValues["id"]?.ToString();
    if (string.IsNullOrEmpty(strId) || !int.TryParse(strId, out int id))
    {
        context.Response.StatusCode = 400;
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync($"<h3> Invalid User ID: \"{strId}\" <h3>");
    }
    else
    {
        var repos = context.RequestServices.GetRequiredService<IUserRepository>();
        User? user = repos.GetByIdUser(id);
        if (user == null)
        {
            context.Response.StatusCode = 404;
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync($"<h3>User with ID \"{id}\" not found</h3>");
        }

        repos.DeleteUser(id);
        context.Response.ContentType = "text/html; charset=utf-8";

        await context.Response.WriteAsync($@"
            <h4>User {user} has been deleted</h4>
            <form action='/' method='get'>
                <button type='submit'> Back to Users List </button>
            </form>");
    }
});

app.MapGet("/users/{id}", async context =>
{
    string strId = context.Request.RouteValues["id"]?.ToString();
    if (string.IsNullOrEmpty(strId) || !int.TryParse(strId, out int id))
    {
        context.Response.StatusCode = 400;
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync($"<h3> Invalid User ID: \"{strId}\" <h3>");
    }
    else
    {
        var repos = context.RequestServices.GetRequiredService<IUserRepository>();
        User? user = repos.GetByIdUser(id);

        if (user == null)
        {
            context.Response.StatusCode = 404;
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync($"<h3>User with ID \"{id}\" not found</h3>");
        }
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync($@"<p> {user} </p>
            <form action='/' method='get'>
                <button type='submit'> Back to Users List </button>
            </form>");

    }



});



app.Run();


public interface IUserRepository
{
    void AddUser(User user);
    void DeleteUser(int id);
    User? GetByIdUser(int id);
    void EditUser(User user);
    List<User> GetAllUsers();
}

public class UserRepository : IUserRepository
{
    private List<User> _users = new List<User>();
    private int _idCounter = 1;     // Счечтик для Ид юзеров.


    public UserRepository()
    {
        _users.Add(new User { Id = _idCounter++, Name = "John", Email = "john@gmail.com" });
        _users.Add(new User { Id = _idCounter++, Name = "Joe", Email = "joe@yahoo.com" });
        _users.Add(new User { Id = _idCounter++, Name = "Jimmy", Email = "jimmy@gmail.com" });
        _users.Add(new User { Id = _idCounter++, Name = "Jessica", Email = "jessica@ahoo.com" });
        _users.Add(new User { Id = _idCounter++, Name = "Julia", Email = "jacob@gmail.com" });
    }

    public void AddUser(User user)
    {
        user.Id = _idCounter++;
        _users.Add(user);
    }
    public void DeleteUser(int id)
    {
        User user = _users.FirstOrDefault(x => x.Id == id);
        if(user != null)
        {
            _users.Remove(user);
        }
    }
    
    public User? GetByIdUser(int id)
    {
        return _users.FirstOrDefault(_ => _.Id == id);
    }

    public void EditUser(User user)
    {
        User currentUser = _users.FirstOrDefault(e => e.Id == user.Id);
        if (currentUser != null)
        {
            currentUser.Name = user.Name;
            currentUser.Email = user.Email;
        }    
    }

    public List<User> GetAllUsers() => _users;

}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Email: {Email}";
    }
}


public static class HtmlGenerator
{

    public static string GenerateUsersPage(List<User> users)
    {
        var sb = new StringBuilder();

        sb.Append("<html><head><style>");
        sb.Append("table { border-collapse: collapse; width: 80%; text-align: left; margin: 20px 0; }");
        sb.Append("th, td { border: 1px solid black; padding: 8px; }");
        sb.Append("th { background-color: #f2f2f2; }");
        sb.Append(".btn { display: inline-block; padding: 10px 20px; color: white; background-color: #007BFF; text-decoration: none; border-radius: 5px; }");
        sb.Append(".btn:hover { background-color: #0056b3; }");
        sb.Append("</style></head><body>");

        sb.Append("<a href='/addUser' class='btn'>Add New User</a>");
        sb.Append("<h2>All Users</h2>");
        sb.Append("<table>");
        sb.Append("<tr><th>Id</th><th>Name</th><th>Email</th><th>Actions</th></tr>");

        foreach (var user in users)
        {
            sb.Append("<tr>");
            sb.Append($"<td>{user.Id}</td>");
            sb.Append($"<td>{user.Name}</td>");
            sb.Append($"<td>{user.Email}</td>");
            sb.Append("<td>");
            sb.Append($"<a href='/users/{user.Id}'>Get</a> | ");
            sb.Append($"<a href='/editUser/{user.Id}'>Edit</a> | ");
            sb.Append($"<a href='/deleteUser/{user.Id}'>Delete</a>");
            sb.Append("</td>");
            sb.Append("</tr>");
        }
        sb.Append("</table>");
        sb.Append("</body></html>");

        return sb.ToString();
    }


    public static string GenerateUserForm(string actionUrl, string buttonText, User? user = null)
    {
        var name = user?.Name ?? string.Empty;
        var email = user?.Email ?? string.Empty;
        var idField = user != null ? $"<input type='hidden' name='id' value='{user.Id}'>" : string.Empty;

        return $@"
        <html>
            <body>
                <h2>{buttonText} User</h2>
                <form action='{actionUrl}' method='post'>
                    {idField}
                    <label for='name'>Name:</label>
                    <input type='text' id='name' name='name' value='{name}' required><br><br>
                    <label for='email'>Email:</label>
                    <input type='email' id='email' name='email' value='{email}' required><br><br>
                    <button type='submit'>{buttonText}</button>
                </form>
            </body>
        </html>";
    }

}