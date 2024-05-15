using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Nac.Mvc.TagHelpers.Base;

namespace Nac.Mvc.TagHelpers;

public class ItemCreateTagHelper : ItemLinkTagHelperBase
{
    public ItemCreateTagHelper(IActionContextAccessor contextAccessor, IUrlHelperFactory urlHelperFactory)
        : base(contextAccessor, urlHelperFactory)
    { }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        BuildContent(output, "Create", "text-success", "", "Neu", "circle-plus");
    }
}