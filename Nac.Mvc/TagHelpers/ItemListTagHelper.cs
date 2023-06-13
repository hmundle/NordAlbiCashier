using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Nac.Mvc.TagHelpers.Base;

namespace Nac.Mvc.TagHelpers;

public class ItemListTagHelper : ItemLinkTagHelperBase
{
    public ItemListTagHelper(IActionContextAccessor contextAccessor, IUrlHelperFactory urlHelperFactory)
        : base(contextAccessor, urlHelperFactory) { }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        BuildContent(output, "Index", "", "", "Show list", "list");
    }
}
