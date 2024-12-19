var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();



//����������, ��� � ��� ���� ������ ����. 
//    ��� �������� �����, �� ������ ������� ������ (List<News>) �������� 
//    � ������ ������� �� �� ��������, ��� �� ����������� ������������ ������.��

//���������� �������� "��������� ������", 
//�� ������� ������������ ����� ������� �������������� ������ ������ �����: ������� ��� ������ ����.
//    ����� ������������ �������� ���� ������ ������ � ��������� ���������, ��� ����������� � Cookie, 
//    ����� ��� ����� ���� ������������ ��� ��������� ��������� �����.




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
