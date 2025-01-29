using HW_21_Delete_Acc_In30Days.QuartzJobs;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddQuartz(e =>
{
    // создаем ключ для работы
    var jobKey = new JobKey("LoggerJob");
    // регистрируем работу с DI контейнером
    e.AddJob<UserDeletionJob>(opts => opts.WithIdentity(jobKey));
    // создаем триггер для работы
    e.AddTrigger(opts => opts
        .ForJob(jobKey) // указываем что выполнять
        .WithIdentity("LoggerJob-trigger") // указываем уникальное имя для триггера
        .WithCronSchedule("0/10 * * * * ?")); // указываем время выполнения - каждые 5 секунд
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);




var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
