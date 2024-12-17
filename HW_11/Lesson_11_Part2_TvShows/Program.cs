using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Lesson_11_Part2_TvShows.Data;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'Context' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();





//Доработать проект «TvShows» следующим образом:

//1. TODO Добавить: пагинацию, сортировку и поиск фильмов. 
//2. + Изменить компонент  «StatisticsViewComponent». Добавить ссылку для каждого жанра, при нажатии по которой, открывается список фильмов данного жанра на новой странице.
//3. TODO Реализовать возможность оставлять комментарии к фильмам всем желающим (указываем имя и комментарий, подтверждаем капчу гугл или свою).
//4. + Создать собственный компонент, который к странице с фильмом, будет выводить 5 случайных фильмов, такого же жанра.












// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TvShows}/{action=Index}/{id?}");

app.Run();
