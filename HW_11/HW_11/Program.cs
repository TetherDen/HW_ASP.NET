var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();



//�������� ����� ����������� ������������, ������� ���������� ��������� �������� ��� ����������� ������������� � ������������ ������:

//1) [ValidateNever]: ��������� ���� "TermsOfService" �� ��������.
//2) [CreditCard]: ��������� ������ ������ ��������� ����� � ���� "CreditCardNumber".
//3) [Compare]: ���������� ������ � ����� "Password" � "ConfirmPassword".
//4) [EmailAddress]: ��������� ������ ������ ����������� ����� � ���� "Email".
//5) [PhoneNumber]: ��������� ������ ������ �������� � ���� "PhoneNumber".
//6) [Range]: ������������ ������� ������������ � ���� "Age" ���������� �� 18 �� 100 ���.
//7) [RegularExpression]: ���������, �������� �� ��� ������������ � ���� "Username" ������ �����, ����� � �������������.
//8) [Required]: ������ ���� "FirstName", "LastName", "Password", "ConfirmPassword" � "Email" �������������.
//9) [StringLength]: ������������ ����� ����� ������������ (���� "Username") 20 ���������, � ������ (���� "Password" � "ConfirmPassword") - 100 ���������.
//10) [URL]: ��������� ������ URL-������ � ���� "Website".
//11) [Remote]: ��������� ������������ ����� ������������ ("Username") ����� ������ ������ �������� �� �������.

//�������� ������ �RegisterViewModel� �� ����������, ���������������� ������������� ���������. �������� �������� � ��������������� ��������� ������.�
//�������� ���������� �AccountController� � ������� �Register�. � ������ Register ���������� ������ �������� ������ ������, ��������� ��������.
//� ������ �������� �������� ������ ��������� ������������ � ���� ������.
//� ������ ������ ���������� ��������� �� ������ ������������.





app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}/{id?}");

app.Run();
