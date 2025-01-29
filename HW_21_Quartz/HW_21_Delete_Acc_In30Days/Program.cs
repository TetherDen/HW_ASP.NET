using HW_21_Delete_Acc_In30Days.QuartzJobs;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddQuartz(e =>
{
    // ������� ���� ��� ������
    var jobKey = new JobKey("LoggerJob");
    // ������������ ������ � DI �����������
    e.AddJob<UserDeletionJob>(opts => opts.WithIdentity(jobKey));
    // ������� ������� ��� ������
    e.AddTrigger(opts => opts
        .ForJob(jobKey) // ��������� ��� ���������
        .WithIdentity("LoggerJob-trigger") // ��������� ���������� ��� ��� ��������
        .WithCronSchedule("0/10 * * * * ?")); // ��������� ����� ���������� - ������ 5 ������
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
