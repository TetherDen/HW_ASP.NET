var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();




// Task 1
//  �������� �������� ��� ����������� ����� �� ����� ������ � ������. 
//  ������������ ������ ������ ����� � ������� ��������� � �������� ������ �� ������ ������������. 
//  ��������� ����������� ������ ������������ ����. ���� ��� ���������� ������ �� �������� Razor Page - .cshtml.



// Task 2
// ���������� 2 ��������, ��� ������� ������������ ���� ������ ��������, 
//  � ������������� ������� � ���������. ���������� ������ ��� ��������� � ����������.





app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
