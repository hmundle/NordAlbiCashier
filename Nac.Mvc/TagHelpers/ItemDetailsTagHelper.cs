using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Nac.Mvc.TagHelpers.Base;

namespace Nac.Mvc.TagHelpers;

public class ItemDetailsTagHelper : ItemLinkTagHelperBase
{
    public ItemDetailsTagHelper(IActionContextAccessor contextAccessor, IUrlHelperFactory urlHelperFactory)
        : base(contextAccessor, urlHelperFactory)
    { }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        BuildContent(output, "Details", "", "", "Show details", "info-circle");
    }
}
