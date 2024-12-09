using HW_09.Service;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ProductService>();

var app = builder.Build();


// Task 1

// У есть веб-приложение для онлайн-магазина. Определите 2 страницы для просмотра и редактирования информации о продукте. 
// Используйте Tag Helper для формы и ссылок, с целью упрощения этого процесса.
// Задействуйте следующие Tag-хелперы:

// AnchorTagHelper
// LinkTagHelper 
// Tag-хелперы форм 
// SelectTagHelper 
// TextAreaTagHelper 
// InputTagHelper
// Используйте asp-href-include и asp-src-include для подключения необходимых стилей и скриптов на страницу.



// Task 2

//У вас есть веб-приложение для отображения космических объектов, и вы хотите добавить необычный Tag-Helper
//для создания анимированных заголовков с использованием CSS-анимации. Давайте назовем его CosmicHeaderTagHelper.
//Его содержимое может выглядеть следующим образом:

//    [HtmlTargetElement("cosmic-header")]
//public class CosmicHeaderTagHelper : TagHelper
//{
//	public string Title { get; set; }

//	public override void Process(TagHelperContext context, TagHelperOutput output)
//	{
//		output.TagName = "h1";
//		output.Attributes.Add("class", "cosmic-header");

//		// Добавьте анимированный заголовок с использованием CSS-анимации
//		output.Content.SetHtmlContent($@"
//                <span class='cosmic-text'>{Title}</span>
//            ");
//	}
//}





app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
