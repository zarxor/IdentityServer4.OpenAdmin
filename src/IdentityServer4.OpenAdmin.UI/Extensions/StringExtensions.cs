//  
//  StringExtensions.cs
//  Copyright (c) Johan Boström. All rights reserved.
//  Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//  

using System.Text.RegularExpressions;

namespace IdentityServer4.OpenAdmin.UI.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            return str.Length > 1
                ? $"{char.ToLowerInvariant(str[0])}{str.Substring(1)}"
                : str.ToLower();
        }

        public static string FromCamelCase(this string str)
        {
            str = str?.Trim();
            if (string.IsNullOrWhiteSpace(str) || str.Length <= 1)
            {
                return str?.ToUpper();
            }

            var output = Regex.Replace(str, "(\\B[A-Z])", " $1").Trim().Substring(1);
            output = $"{str.ToUpper()[0]}{output}";

            return output;
        }
    }
}