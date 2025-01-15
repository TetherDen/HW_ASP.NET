using EmailService;
using EmailService.Interfaces;
using EmailService.Models;
using HW_15_Email_FutureMe.Data;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


//=================================

var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();

builder.Services.AddSingleton(emailConfig);


// Install-Package MailKit                              // Add To EmailService  .dll
// Install-Package Microsoft.AspNetCore.Http.Features   // Add To EmailService  .dll

builder.Services.AddScoped<IEmailSender, EmailSend>();

//  Мы используем класс BodyBuilder для создания тела письма, добавляем файлы вложений, 
//  преобразуя их в байтовые массивы, и добавляем их в Attachments. 
//  Конфигурация FormOptions устанавливает максимальные лимиты для обработки больших данных и файлов.
//  В Конспекте подробнее...
builder.Services.Configure<FormOptions>(o => {
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});



builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
});

//=================================

builder.Services.AddControllersWithViews();

var app = builder.Build();

//===================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationContext>();
        //DbInitializer.Init(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error while init DB...");
    }
}
//===================

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
