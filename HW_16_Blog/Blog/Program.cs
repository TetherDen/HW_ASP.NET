using Blog.Data;
using Blog.Interfaces;
using Blog.Models;
using Blog.Repository;
using EmailService.Models;
using EmailService;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EmailService.Interfaces;

var builder = WebApplication.CreateBuilder(args);


//Install - Package Microsoft.AspNetCore.Identity.EntityFrameworkCore
//Install-Package Microsoft.EntityFrameworkCore.SqlServer 

// Практика 2
//  У пользователя есть возможность просматривать наши публикации, если не обращать внимание на контент, 
//    страницы достаточно пустая, скучная. Разработайте компонент, который будет выводить на страницу публикации, 
//    4 случайных публикации для завлечения пользователя. Это действие позволит повысить поведенческие факторы для нашего сайта.


builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationContext>(opts =>
{
    opts.UseSqlServer(
        builder.Configuration["ConnectionStrings:DefaultConnection"]);
});

builder.Services.AddIdentity<User, IdentityRole>(options =>     // Потом добавили опции для пароля (убрали ограничения для пароля + длина пароля).
{
    options.Password.RequiredLength = 3;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
})
    .AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddTransient<IMembership, MembershipRepository>();
builder.Services.AddTransient<ICategory, CategoryRepository>();
builder.Services.AddTransient<IPublication, PublicationRepository>();
builder.Services.AddTransient<ISubscription, SubscriptionRepository>();  // HW Подписки



builder.Services.AddTransient<IMembership, MembershipRepository>();

// HW Email Notification
builder.Services.AddScoped<IPublicationEmailNotification, PublicationEmailNotificationRepository>();

//=================================================
// Добавил в проект ранее сделанную .dll  EmailService  для отправки Email писем с использованием MailKit


var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();

builder.Services.AddSingleton(emailConfig);

builder.Services.AddScoped<IEmailSender, EmailSend>();

builder.Services.Configure<FormOptions>(o => {
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});



//=========================================

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var applicationContext = services.GetRequiredService<ApplicationContext>();
        await RoleInitializer.InitializeAsync(userManager, rolesManager);
        await ContentInitializer.InitializeAsync(applicationContext);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}




app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();    // +. Подключении аунтефикации
app.UseAuthorization();     // +  Подклчюений авторизации

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
