using HW_17_Mini_Blog.Data;
using HW_17_Mini_Blog.Interfaces;
using HW_17_Mini_Blog.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDefaultIdentity<IdentityUser>(opts =>
{
    opts.SignIn.RequireConfirmedAccount = true;
    opts.Password.RequiredLength = 3;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IPublication, PublicationRepository>();


//Создайте проект по типу: Asp.Net Core MVC Individual Accounts, с системой Identity.
//Выполните следующие задачи:

//1) Используйте систему Identity для регистрации новых пользователей и аутентификации уже существующих.
//2) Определите модель для статей блога, которая будет содержать поля, такие как заголовок, описание, дата публикации и автор статьи.
//3) Создайте контроллер и представление для отображения списка всех статей блога на главной странице. Чтобы пользователи могли просматривать все доступные статьи.
//4) Реализуйте возможность добавления новых статей блога через веб-интерфейс. Только аутентифицированные пользователи должны иметь доступ к этой функции.
//5) Создайте страницу профиля пользователя, на которой отображается информация о пользователе и список его опубликованных статей. Информация о пользователя должна быть редактируемой.





var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
