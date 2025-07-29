// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


namespace CocktailsApp.API.Extensions 
{
    public static class EnumExtensions
    {
        public static TEnum ToEnum<TEnum>(this string input) where TEnum : struct, Enum
        {
            if (!Enum.TryParse<TEnum>(input, ignoreCase: true, out var output))
                throw new ArgumentException("Enumeration conversion failed");

            return output;
        }
    }
}
