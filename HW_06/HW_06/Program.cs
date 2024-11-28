using HW_06.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();




//У нас есть веб-приложение для онлайн-магазина, и мы хотим реализовать функциональность поиска товаров.
//Для этого мы можем создать контроллер ProductController, который будет обрабатывать запросы на поиск товаров.
//Контроллер ProductController должен иметь следующие действия: 

//Index() - метод, который будет возвращать список всех товаров на сайте в виде JSON;
//Create() - метод, который будет добавлять товар, через POST запрос.
//Для этого используйте HTML страницу или действие возвращающее соответствующую форму для заполнения данных о товаре.
//Search(string keyword) - метод, который будет искать товары по ключевому слову и возвращать их в виде JSON;
//Details(int id) - метод, который будет отображать подробную информацию о товаре по его идентификатору в виде JSON;
//Delete(int id) - метод, который будет удалять товар по его идентификатору, через POST запрос и в качестве ответа возвращать удаленный товар виде JSON.

//Данные о товарах, хранить в базе данных.


// add EF core DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var context = services.GetRequiredService<ApplicationDbContext>();

	//context.Database.EnsureDeleted();
	context.Database.EnsureCreated();
	DbInitializer.Init(context);
}

//-------------------------



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
