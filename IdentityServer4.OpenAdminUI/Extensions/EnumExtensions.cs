// 
//  EnumExtensions.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdentityServer4.OpenAdminUI.Extensions
{
    public static class EnumExtensions
    {
        public static IEnumerable<SelectListItem> AsSelectListItems(this Enum thisEnum)
        {
            var enumType = thisEnum.GetType();
            return Enum
                .GetValues(enumType)
                .Cast<object>()
                .Select(value => new SelectListItem
                {
                    Text = Enum.GetName(enumType, value),
                    Value = value.ToString()
                });
        }
    }
}