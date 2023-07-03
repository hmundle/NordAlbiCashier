using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Nac.Mvc.TagHelpers.Base;

namespace Nac.Mvc.TagHelpers;

public class ItemDeleteTagHelper : ItemLinkTagHelperBase
{
    public ItemDeleteTagHelper(IActionContextAccessor contextAccessor, IUrlHelperFactory urlHelperFactory)
        : base(contextAccessor, urlHelperFactory)
    { }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        //<a asp-action="Delete" asp-route-id="@item.Id" class="text-danger">Delete <i class="fas fa-trash"></i></a>
        BuildContent(output, "Delete", "text-danger", "", "Löschen", "trash");
    }
}