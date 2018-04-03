// 
//  HtmlHelperExtensions.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;

namespace IdentityServer4.OpenAdminUI.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent CheckBoxListFor<TModel, TProperty>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, IEnumerable<TProperty>>> expression,
            IEnumerable<SelectListItem> multiSelectList,
            object htmlAttributes = null)
        {
            // Derive property name for checkbox name
            if (!(expression.Body is MemberExpression body)) return null;

            var propertyName = body.Member.Name;

            // Get currently select values from the ViewData model
            var list = expression.Compile().Invoke(htmlHelper.ViewData.Model);

            // Convert selected value list to a List<String> for easy manipulation
            var selectedValues = new List<string>();

            if (list != null) selectedValues = new List<TProperty>(list).ConvertAll(i => i.ToString());

            // Create div
            var ulTag = new TagBuilder("ul");
            ulTag.AddCssClass("checkBoxList");
            ulTag.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);

            // Add checkboxes
            foreach (var item in multiSelectList)
                ulTag.InnerHtml.AppendHtml(string.Format(
                    "<li><input type=\"checkbox\" name=\"{0}\" id=\"{0}_{1}\" value=\"{1}\" {2} /><label for=\"{0}_{1}\">{3}</label></li>",
                    propertyName,
                    item.Value,
                    selectedValues.Contains(item.Value) ? "checked=\"checked\"" : "",
                    item.Text));

            return ulTag;
        }
    }
}