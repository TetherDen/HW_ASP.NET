using HW_10.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationContext>();
        DbInitializer.Init(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error while init DB...");
    }
}



//Разработайте веб-приложение для онлайн-магазина книг. Пользователи могут просматривать каталог книг и оставлять комментарии.
//Функциональные требования:
//1) Просмотр каталога книг: Пользователи могут просматривать каталог всех доступных книг с информацией о названии, авторе, жанре и цене.
//2) Публикация комментария: Пользователи могут оставлять комментарии для любой из книг. Сами комментарии отображаются внизу страницы с книгой.
//3) Взаимодействие с Entity Framework Core: Информации про книги и комментарии должных хранится в базе данных.
//     Управление базой данных осуществляется через EF Core.







app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Index}/{id?}");

app.Run();
