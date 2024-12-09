using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HW_09.TagHelpers
{
	[HtmlTargetElement("cosmic-header")]
	public class CosmicHeaderTagHelper : TagHelper
	{
		public string Title { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "h1";
			output.Attributes.Add("class", "cosmic-header");

			// Добавьте анимированный заголовок с использованием CSS-анимации
			output.Content.SetHtmlContent($@"
                <span class='cosmic-text'>{Title}</span>
            ");
		}
	}
}
