using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Lesson_11_Part2_TvShows.Data;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'Context' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();





//���������� ������ �TvShows� ��������� �������:

//1. TODO ��������: ���������, ���������� � ����� �������.�
//2. + �������� ��������� �StatisticsViewComponent�. �������� ������ ��� ������� �����, ��� ������� �� �������, ����������� ������ ������� ������� ����� �� ����� ��������.
//3. TODO ����������� ����������� ��������� ����������� � ������� ���� �������� (��������� ��� � �����������, ������������ ����� ���� ��� ����).
//4. + ������� ����������� ���������, ������� � �������� � �������, ����� �������� 5 ��������� �������, ������ �� �����.












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
