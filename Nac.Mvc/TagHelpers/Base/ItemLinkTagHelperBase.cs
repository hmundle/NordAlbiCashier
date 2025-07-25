﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Nac.Mvc.TagHelpers.Base;

public abstract class ItemLinkTagHelperBase : TagHelper
{
    protected readonly IUrlHelper UrlHelper;

    protected ItemLinkTagHelperBase(
        IActionContextAccessor contextAccessor,
        IUrlHelperFactory urlHelperFactory)
    {
        UrlHelper = urlHelperFactory.GetUrlHelper(contextAccessor.ActionContext!);
    }

    public Guid? ItemId { get; set; }
    public String? ItemControllerName { get; set; } = null; // null is the current controller

    protected void BuildContent(TagHelperOutput output,
        string actionName,
        string cssClassName,
        string displayText,
        string tooltip,
        string fontAwesomeName)
    {
        output.TagName = "a"; // Replaces <item-list> with <a> tag
        var target = (ItemId.HasValue)
        ? UrlHelper.Action(actionName, ItemControllerName, new { id = ItemId })
        : UrlHelper.Action(actionName, ItemControllerName);
        output.Attributes.SetAttribute("href", target);
        output.Attributes.Add("class", cssClassName + " px-2 d-print-none");
        output.Attributes.Add("title", tooltip);
        output.Content.AppendHtml($@"{displayText} <i class=""lead fa-solid fa-{fontAwesomeName}""></i>");
    }

}
