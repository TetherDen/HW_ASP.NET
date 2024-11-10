using Microsoft.Extensions.Hosting;
using System;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;



var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

List<Person> persons = new List<Person>
{
    new Person("John", "john@gmail.com", "12345"),
    new Person("Joe", "Joe@gmail.com", "45365134"),
    new Person("Jack", "Jack@gmail.com", "2341"),
};

app.Run(async (context) =>
{

    // Task 0
    if (context.Request.Path == "/invite" && context.Request.Method == "POST")
    {
        //var form = context.Request.Form;
        var form = await context.Request.ReadFormAsync();
        string name = form["name"];
        string email = form["email"];
        string phone = form["phone"];
        persons.Add(new Person(name, email, phone));
        Console.WriteLine($"Person Added:  {name} {email} {phone}");
        context.Response.Redirect("/thnks.html");
    }
    //Task 1
    //Реализовать обработку POST запроса с возвращением ответа.Когда вы запустите приложение и сделаете POST - запрос к URL "/api/greeting" с телом запроса,
    //содержащим строку(например, { "name": "John"}), вы должны получить ответ с персонализированным приветствием (например, "Hello, John!").

    else if (context.Request.Path == "/api/greeting" && context.Request.Method == "POST")
    {
        var requestData = await context.Request.ReadFromJsonAsync<Dictionary<string, JsonElement>>();
        if(requestData != null && requestData.ContainsKey("name"))
        {
            string name = requestData["name"].GetString();
            await context.Response.WriteAsync($"Hello {name}");
        }
    }

    //Task 2
    //Используя JavaScript или любой другой Фреймворк или PostMan, отправьте JSON объект на любое действие в C#. После получение, отобразите это значение на новой странице.
    else if (context.Request.Path == "/showJson" && context.Request.Method == "POST")
    {
        try
        {
            dynamic? requestData = await context.Request.ReadFromJsonAsync<dynamic>();
            await context.Response.WriteAsync($"ServerResponse:\n {requestData}");
        }
        catch (JsonException ex)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync($"Invalid JSON format: {ex.Message}");
        }
    }

    //Создайте приложение возвращающие список пользователей из коллекции или базы данных в ответ на GET запрос.
    //Сам GET-запрос направляем на URL "/api/customers" в лямбда-функцию, которая извлекает всех клиентов из базы данных или коллекции и возвращает их в качестве ответа.
    else if(context.Request.Path == "/api/customers" && context.Request.Method == "GET")
    {
        await context.Response.WriteAsJsonAsync(persons);
    }

    else
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("404 Not Found");
    }
});

app.Run();

public struct Person(string name, string email, string phone)
{
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
    public string Phone { get; set; } = phone;
}