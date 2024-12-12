using HW_10.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationContext>();
        DbInitializer.Init(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error while init DB...");
    }
}



//������������ ���-���������� ��� ������-�������� ����. ������������ ����� ������������� ������� ���� � ��������� �����������.
//�������������� ����������:
//1) �������� �������� ����: ������������ ����� ������������� ������� ���� ��������� ���� � ����������� � ��������, ������, ����� � ����.
//2) ���������� �����������: ������������ ����� ��������� ����������� ��� ����� �� ����. ���� ����������� ������������ ����� �������� � ������.
//3) �������������� � Entity Framework Core: ���������� ��� ����� � ����������� ������� �������� � ���� ������.
//     ���������� ����� ������ �������������� ����� EF Core.







app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Index}/{id?}");

app.Run();
