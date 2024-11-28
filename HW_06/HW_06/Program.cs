using HW_06.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();




//� ��� ���� ���-���������� ��� ������-��������, � �� ����� ����������� ���������������� ������ �������.
//��� ����� �� ����� ������� ���������� ProductController, ������� ����� ������������ ������� �� ����� �������.
//���������� ProductController ������ ����� ��������� ��������:�

//Index() - �����, ������� ����� ���������� ������ ���� ������� �� ����� � ���� JSON;
//Create() - �����, ������� ����� ��������� �����, ����� POST ������.
//��� ����� ����������� HTML �������� ��� �������� ������������ ��������������� ����� ��� ���������� ������ � ������.
//Search(string keyword) - �����, ������� ����� ������ ������ �� ��������� ����� � ���������� �� � ���� JSON;
//Details(int id) - �����, ������� ����� ���������� ��������� ���������� � ������ �� ��� �������������� � ���� JSON;
//Delete(int id) - �����, ������� ����� ������� ����� �� ��� ��������������, ����� POST ������ � � �������� ������ ���������� ��������� ����� ���� JSON.

//������ � �������, ������� � ���� ������.


// add EF core DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var context = services.GetRequiredService<ApplicationDbContext>();

	//context.Database.EnsureDeleted();
	context.Database.EnsureCreated();
	DbInitializer.Init(context);
}

//-------------------------



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
