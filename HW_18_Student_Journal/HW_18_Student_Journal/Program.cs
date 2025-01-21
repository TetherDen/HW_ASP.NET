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
//Install - Package Microsoft.EntityFrameworkCore.Tools	// Tools - ��� ������ � ����������. 


builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
});



builder.Services.AddIdentity<User, IdentityRole>(opts =>
{
    opts.Password.RequiredLength = 3;   // ����������� �����
    opts.Password.RequireNonAlphanumeric = false;   // ��������� �� �� ���������-�������� �������
    opts.Password.RequireLowercase = false; // ��������� �� ������� � ������ ��������
    opts.Password.RequireUppercase = false; // ��������� �� ������� � ������� ��������
    opts.Password.RequireDigit = false; // ��������� �� �����

})
    .AddEntityFrameworkStores<ApplicationContext>();


// ������ �������� ����������� � appsettings
builder.Services.Configure<TeachersEmails>(builder.Configuration.GetSection("TeachersEmails"));


builder.Services.AddTransient<IGroup, GroupRepository>();
builder.Services.AddScoped<ISubject, SubjectRepository>();

// ��������� ������, ����� ����� ������?
builder.Services.AddScoped<IsTeacherActionFilter>();

//======================================================================================

//�������� ���-���������� �� ������ ASP.NET MVC, ������� ����� ������������ ����� ������-������ ��� ���������. �������� ������ ����������������, ������� � �������, ������������� � ��������� ���� ������������� ����������.
//���������� � ����������:

//1) ����������� � �����������:
// - �������� �������� �����������, ��� �������� ������ ������� ������� ������, ����� ���� ������������ ������ (���, �������, ����� ����������� ����� � �. �.).�
// - ���������� �������� �����, ��� �������� ������ ������������ ���� ������� ������ ��� ������� � �������.
//2) ������� ������������:
// - ����� ����� �������� ������ ������ ���� ������������ �������, ��� ������������ �� ������.
// - ���������� ����������� ������.
//3) �������� ������������� ����������:
// - �������� ��������, �� ������� �������� ����� ������ ������ ����� ��������� � ������ �� ������� ��������.
// - ����������� ��������� ������� ������ ��������.
//4) �������� ������������� ����������:

//�������� ��������� � ����������� ������, ����� ������������ ����� ���� �������������, ������ ������� ��������� � ���������������� ����� appsettings.json. � ���� ������, ���� � �������� �� ������������.

//======================================================================================

var app = builder.Build();

// statuscode pages  (� ������� ����)
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
