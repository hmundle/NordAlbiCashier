using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Nac.Mvc.TagHelpers;

public class CopyTextTagHelper : TagHelper
{
    public String? ElementId { get; set; } = string.Empty;

    protected void BuildContent(TagHelperOutput output,
                                string cssClassName,
                                string displayText,
                                string fontAwesomeName)
    {
        output.TagName = "a"; // Replaces <item-list> with <a> tag
        var target = $@"javascript:CopyTextFromElement(""{ElementId}"")";
        output.Attributes.SetAttribute("href", target);
        output.Attributes.Add("class", cssClassName + " px-2");
        output.Attributes.Add("title", "Copy content into clipboard");
        output.Content.AppendHtml($@"{displayText} <i class=""fas fa-{fontAwesomeName}""></i>");
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        BuildContent(output, "", "", "copy");
    }
}
