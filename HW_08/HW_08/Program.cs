using HW_08.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<BookService>();

var app = builder.Build();


//Task 1
//�������� ���-���������� ��� ����������� ������ ���� � ������������ ���������� ����� ����.

//1) �������� Model �Book�, ������� ����� ������������ ����� �� ����������, ������ ��� Id, Title, Author, Genre, Year, � �.�.

//2) �������� ViewModel �BookViewModel�, ������� ����� �������������� ��� �������� ������ � ������ 
//����� ������������� � ���������������. 
//� ���� ViewModel ����� ���� ������ �� ����, ������� ���������� ��� ����������� � ����� �������������, ��������, Title, Author, Genre, Year.

//3) �������� ����� �BookService�, ������� ����� ��������� ��������� ���� � ������������� � ��� ������. 
//������ ����� �������� � ��������� ��������, � ��������� ������ - Singleton. � �������� ������������, ������ ������������ ���� ������ ����� ADO.NET.

//4) �������� ���������� �BookController�, ������� ����� �������� �� ����������� �������� �� ������� ���� � ���������� ����� ����.�
//�������� �������� ��� ����������� ������ ���� (Index) � �������� ��� ���������� ����� ����� (Add).

//5) �������� ������������� Index.cshtml, ������� ����� ���������� ������ ����. 
//�������� ������������� Add.cshtml, ������� ����� �������������� ��� ���������� ����� �����.



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Index}/{id?}");

app.Run();
