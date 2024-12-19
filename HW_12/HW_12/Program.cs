var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();



//Представим, что у вас есть личный Блог. 
//    Для имитации блога, вы можете создать список (List<News>) новостей 
//    и просто вывести их на страницу, так же используйте элементарный шаблон.  

//Реализуйте страницу "Настройки чтения", 
//на которой пользователь может выбрать предпочитаемый способ чтения блога: светлую или темную тему.
//    Когда пользователь выбирает свой способ чтения и сохраняет настройки, они сохраняются в Cookie, 
//    чтобы они могли быть использованы при следующем посещении сайта.




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
