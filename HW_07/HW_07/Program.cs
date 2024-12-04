var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();




// Task 1
//  Создайте страницу для конвертации суммы из одной валюты в другую. 
//  Пользователь должен ввести сумму и выбрать начальную и конечную валюты из списка предложенных. 
//  Результат конвертации должен отображаться ниже. Весь код выполняйте только на странице Razor Page - .cshtml.



// Task 2
// Определить 2 действия, для каждого использовать свою мастер страницу, 
//  с отличающимися стилями и разметкой. Разместить ссылки для навигации в приложении.





app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
