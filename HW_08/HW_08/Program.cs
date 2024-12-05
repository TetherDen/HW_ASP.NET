using HW_08.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<BookService>();

var app = builder.Build();


//Task 1
//Создайте веб-приложения для отображения списка книг с возможностью добавления новых книг.

//1) Создайте Model «Book», которая будет представлять книгу со свойствами, такими как Id, Title, Author, Genre, Year, и т.д.

//2) Создайте ViewModel «BookViewModel», который будет использоваться для передачи данных о книгах 
//между контроллерами и представлениями. 
//В этой ViewModel могут быть только те поля, которые необходимы для отображения и ввода пользователем, например, Title, Author, Genre, Year.

//3) Создайте класс «BookService», который будет содержать коллекцию книг и опосредствует к ним доступ. 
//Данный класс добавьте в коллекцию сервисов, с жизненным циклом - Singleton. В качестве альтернативы, можете использовать базу данных через ADO.NET.

//4) Создайте контроллер «BookController», который будет отвечать за отображение страницы со списком книг и добавление новых книг. 
//Добавьте действие для отображения списка книг (Index) и действие для добавления новой книги (Add).

//5) Создайте представление Index.cshtml, которое будет отображать список книг. 
//Создайте представление Add.cshtml, которое будет использоваться для добавления новой книги.



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Index}/{id?}");

app.Run();
