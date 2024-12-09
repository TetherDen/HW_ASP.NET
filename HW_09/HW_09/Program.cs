using HW_09.Service;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ProductService>();

var app = builder.Build();


// Task 1

// � ���� ���-���������� ��� ������-��������. ���������� 2 �������� ��� ��������� � �������������� ���������� � ��������.�
// ����������� Tag Helper ��� ����� � ������, � ����� ��������� ����� ��������.
// ������������ ��������� Tag-�������:

// AnchorTagHelper
// LinkTagHelper�
// Tag-������� ����
// SelectTagHelper�
// TextAreaTagHelper�
// InputTagHelper
// ����������� asp-href-include � asp-src-include ��� ����������� ����������� ������ � �������� �� ��������.



// Task 2

//� ��� ���� ���-���������� ��� ����������� ����������� ��������, � �� ������ �������� ��������� Tag-Helper
//��� �������� ������������� ���������� � �������������� CSS-��������. ������� ������� ��� CosmicHeaderTagHelper.
//��� ���������� ����� ��������� ��������� �������:

//����[HtmlTargetElement("cosmic-header")]
//public class CosmicHeaderTagHelper : TagHelper
//{
//	public string Title { get; set; }

//	public override void Process(TagHelperContext context, TagHelperOutput output)
//	{
//		output.TagName = "h1";
//		output.Attributes.Add("class", "cosmic-header");

//		// �������� ������������� ��������� � �������������� CSS-��������
//		output.Content.SetHtmlContent($@"
//����������������<span class='cosmic-text'>{Title}</span>
//������������");
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
