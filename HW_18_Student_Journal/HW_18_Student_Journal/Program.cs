using HW_18_Student_Journal.Data;
using HW_18_Student_Journal.Filters;
using HW_18_Student_Journal.Interface;
using HW_18_Student_Journal.Models;
using HW_18_Student_Journal.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();



//Install - Package Microsoft.EntityFrameworkCore.SqlServer
//Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore 
//Install - Package Microsoft.EntityFrameworkCore.Tools	// Tools - Для работы с миграциями. 


builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
});



builder.Services.AddIdentity<User, IdentityRole>(opts =>
{
    opts.Password.RequiredLength = 3;   // минимальная длина
    opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
    opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
    opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
    opts.Password.RequireDigit = false; // требуются ли цифры

})
    .AddEntityFrameworkStores<ApplicationContext>();


// Имейлы учителей определнные в appsettings
builder.Services.Configure<TeachersEmails>(builder.Configuration.GetSection("TeachersEmails"));


builder.Services.AddTransient<IGroup, GroupRepository>();
builder.Services.AddScoped<ISubject, SubjectRepository>();

// Кастомный Фильтр, пусть будет скопед?
builder.Services.AddScoped<IsTeacherActionFilter>();

//======================================================================================

//Создайте веб-приложение на основе ASP.NET MVC, которое будет представлять собой онлайн-журнал для студентов. Студенты смогут регистрироваться, входить в систему, просматривать и оценивать свои академические достижения.
//Требования к приложению:

//1) Регистрация и авторизация:
// - Создайте страницу регистрации, где студенты смогут создать учетную запись, вводя свои персональные данные (имя, фамилия, адрес электронной почты и т. д.). 
// - Реализуйте страницу входа, где студенты смогут использовать свои учетные данные для доступа к системе.
//2) Профиль пользователя:
// - После входа студенты должны видеть свой персональный профиль, где отображаются их данные.
// - Реализуйте возможность выхода.
//3) Просмотр академических достижений:
// - Создайте страницу, на которой студенты могут видеть список своих предметов и оценок по каждому предмету.
// - Возможность просмотра истории оценок студента.
//4) Создание академических достижений:

//Создание предметов и выставление оценок, может осуществлять узкий круг пользователей, емейлы которых находятся в конфигурационном файле appsettings.json. В этой задачи, роли и политики не использовать.

//======================================================================================

var app = builder.Build();

// statuscode pages  (в фильтре юзаю)
app.UseStatusCodePages();



app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
