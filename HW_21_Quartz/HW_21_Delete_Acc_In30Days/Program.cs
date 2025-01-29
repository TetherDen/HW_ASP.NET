using HW_21_Delete_Acc_In30Days.QuartzJobs;
using Quartz;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

//Реализовать систему, которая автоматически удаляет профили пользователей окончательно после того, как они запросили удаление своего аккаунта. Удаление должно произойти спустя 30 дней после запроса на удаление.

//1) Создайте модель для профилей пользователей с необходимыми полями, такими как: UserId, Username, DeletedAt и другими данными. 

//В базе данных добавьте поле DeletedAt для отслеживания времени запроса на удаление профиля. Реализуйте механизм, который будет устанавливать DeletedAt в текущее время при запросе на удаление профиля.

//2) Настройте задачу по расписанию, которая будет выполняться, например, каждый день. 
//В методе, вызываемом по расписанию, получите все профили пользователей, у которых DeletedAt не равен null, и которым прошло более 30 дней с момента удаления.
//Удалите окончательно эти профили из базы данных.

//3) Реализуйте автоматическую отправку уведомлений пользователям за неделю до окончательного удаления профиля.
//4) Фиксируйте результаты выполнения задачи в логах или другом удобном месте.


// NuGet Библиотеки:
// Quartz.AspNetCore
// Quartz.Extensions.DependencyInjection


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
        .WithCronSchedule("0/10 * * * * ?")); // указываем время выполнения - каждые 10 секунд

        //.WithCronSchedule("0 0 12 * * ?")); // или каждый день в 12:00
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
