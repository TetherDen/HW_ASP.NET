using HW_21_Delete_Acc_In30Days.QuartzJobs;
using Quartz;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

//����������� �������, ������� ������������� ������� ������� ������������� ������������ ����� ����, ��� ��� ��������� �������� ������ ��������. �������� ������ ��������� ������ 30 ���� ����� ������� �� ��������.

//1) �������� ������ ��� �������� ������������� � ������������ ������, ������ ���: UserId, Username, DeletedAt � ������� �������.�

//� ���� ������ �������� ���� DeletedAt ��� ������������ ������� ������� �� �������� �������. ���������� ��������, ������� ����� ������������� DeletedAt � ������� ����� ��� ������� �� �������� �������.

//2) ��������� ������ �� ����������, ������� ����� �����������, ��������, ������ ����.�
//� ������, ���������� �� ����������, �������� ��� ������� �������������, � ������� DeletedAt �� ����� null, � ������� ������ ����� 30 ���� � ������� ��������.
//������� ������������ ��� ������� �� ���� ������.

//3) ���������� �������������� �������� ����������� ������������� �� ������ �� �������������� �������� �������.
//4) ���������� ���������� ���������� ������ � ����� ��� ������ ������� �����.


// NuGet ����������:
// Quartz.AspNetCore
// Quartz.Extensions.DependencyInjection


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
        .WithCronSchedule("0/10 * * * * ?")); // ��������� ����� ���������� - ������ 10 ������

        //.WithCronSchedule("0 0 12 * * ?")); // ��� ������ ���� � 12:00
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
