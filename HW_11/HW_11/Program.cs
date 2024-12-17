var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();



//Создайте форму регистрации пользователя, которая использует следующие атрибуты для обеспечения достоверности и безопасности данных:

//1) [ValidateNever]: Исключает поле "TermsOfService" из проверки.
//2) [CreditCard]: Проверяет формат номера кредитной карты в поле "CreditCardNumber".
//3) [Compare]: Сравнивает пароли в полях "Password" и "ConfirmPassword".
//4) [EmailAddress]: Проверяет формат адреса электронной почты в поле "Email".
//5) [PhoneNumber]: Проверяет формат номера телефона в поле "PhoneNumber".
//6) [Range]: Ограничивает возраст пользователя в поле "Age" диапазоном от 18 до 100 лет.
//7) [RegularExpression]: Проверяет, содержит ли имя пользователя в поле "Username" только буквы, цифры и подчеркивания.
//8) [Required]: Делает поля "FirstName", "LastName", "Password", "ConfirmPassword" и "Email" обязательными.
//9) [StringLength]: Ограничивает длину имени пользователя (поле "Username") 20 символами, а пароля (поля "Password" и "ConfirmPassword") - 100 символами.
//10) [URL]: Проверяет формат URL-адреса в поле "Website".
//11) [Remote]: Проверяет уникальность имени пользователя ("Username") путем вызова метода действия на сервере.

//Создайте модель «RegisterViewModel» со свойствами, соответствующими перечисленным атрибутам. Добавьте атрибуты к соответствующим свойствам модели. 
//Создайте контроллер «AccountController» с методом «Register». В методе Register реализуйте логику проверки данных модели, используя атрибуты.
//В случае успешной проверки данных сохраните пользователя в базе данных.
//В случае ошибки отобразите сообщение об ошибке пользователю.





app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}/{id?}");

app.Run();
