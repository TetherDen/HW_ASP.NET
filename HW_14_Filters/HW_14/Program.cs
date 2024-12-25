using HW_14.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();





// У вас есть веб-приложение для онлайн магазина. Вам необходимо реализовать функциональность, 
//  которая позволит пользователю выбирать валюту, в которой будут отображаться цены товаров. 

// Ваша задача состоит в том, чтобы использовать фильтры ресурсов в ASP.NET MVC для поддержки мультивалютности. 
// Создайте фильтр ресурсов, который будет определять текущую выбранную валюту пользователя и загружать информацию о выбранной валюте, в базу данных. 

// Фильтр ресурсов должен быть применен к действию, которое отвечает за выбор конкретной валюты.





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
        //DbInit.Init(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error while init DB...");
    }
}




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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
