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


//�������� ������ �� ����: Asp.Net Core MVC Individual Accounts, � �������� Identity.
//��������� ��������� ������:

//1) ����������� ������� Identity ��� ����������� ����� ������������� � �������������� ��� ������������.
//2) ���������� ������ ��� ������ �����, ������� ����� ��������� ����, ����� ��� ���������, ��������, ���� ���������� � ����� ������.
//3) �������� ���������� � ������������� ��� ����������� ������ ���� ������ ����� �� ������� ��������. ����� ������������ ����� ������������� ��� ��������� ������.
//4) ���������� ����������� ���������� ����� ������ ����� ����� ���-���������. ������ ������������������� ������������ ������ ����� ������ � ���� �������.
//5) �������� �������� ������� ������������, �� ������� ������������ ���������� � ������������ � ������ ��� �������������� ������. ���������� � ������������ ������ ���� �������������.





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
