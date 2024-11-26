using HW_05.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;




var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// для DbInit
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DBInitializer.DbInit(dbContext);
}



app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;
    var db = context.RequestServices.GetRequiredService<ApplicationDbContext>();
    response.ContentType = "text/html; charset=utf-8";
    string? token = context.Request.Query["token"];


    if (request.Path == "/" && request.Method == "GET")
    {
        await response.WriteAsync(HtmlPageGenerator.GenerateLoginPage());
    }

    else if (request.Path == "/login" && request.Method == "POST")
    {

        string email = request.Form["Email"];
        string password = request.Form["Password"];

        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
        {
            User user = db.Users.FirstOrDefault(e => e.Email == email && e.Password == password);
            if(user != null)
            {
                token = user.Token;
                response.Redirect($"/myServices?token={token}");
            }
            else
            {
                await response.WriteAsync("<a href='/'>Invalid email or password</a>");
            }
        }
    }

    // Register
    else if (request.Path == "/register" && request.Method == "GET")
    {
        await response.WriteAsync(HtmlPageGenerator.GenerateRegisterPage());
    }

    else if (request.Path == "/register" && request.Method == "POST")
    {
        string name = request.Form["Name"];
        string email = request.Form["Email"];
        string password = request.Form["Password"];

        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(name))
        {
            var existingUser = db.Users.FirstOrDefault(u => u.Email == email);
            if(existingUser == null)
            {
                User newUser = new User
                {
                    Name = name,
                    Email = email,
                    Password = password,
                    Token = Guid.NewGuid().ToString()
                };
                db.Users.Add(newUser);
                await db.SaveChangesAsync();
                response.Redirect("/");

            }
            else
            {
                await response.WriteAsync("<a href='/'>This email allready registered</a>");
            }
        }
        else
        {
            await response.WriteAsync("<a href='/'>All fields are required</a>");
        }
    }


    // TOKEN Validate
    else if (string.IsNullOrEmpty(token) || !IsTokenValid(db, token))
    {
        await response.WriteAsync(HtmlPageGenerator.GenerateLoginPage());
    }


    else if (request.Path == "/myServices" && request.Method == "GET")
    {
        var userServices = db.Services.Where(s => s.UserToken == token).ToList();

        await response.WriteAsync(HtmlPageGenerator.GenerateMyServicesPage(userServices,token));
    }

    else if (request.Path == "/newService" && request.Method == "GET")
    {
        await response.WriteAsync(HtmlPageGenerator.GenerateNewServiceForm(token));
    }

    else if (request.Path == "/newService" && request.Method == "POST")
    {
        string name = request.Form["Name"];
        string phoneNumber = request.Form["PhoneNumber"];
        string description = request.Form["Description"];

        var newService = new CustomerService
        {
            Name = name,
            PhoneNumber = phoneNumber,
            Description = description,
            RegistrationDate = DateTime.Now,
            UserToken = token
        };
        db.Services.Add(newService);
        await db.SaveChangesAsync();

        response.Redirect($"/myServices?token={token}");
    }



    else
    {
        response.StatusCode = 404;
        await response.WriteAsync("<a href='/'>Page Not Found</a>");
    }
});


app.Run();


// TODO Вынести метод в класс
bool IsTokenValid(ApplicationDbContext db, string? token)
{
    return db.Users.Any(u => u.Token == token);
}