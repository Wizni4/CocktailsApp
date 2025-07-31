// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.API.Shared;

namespace CocktailsApp.API.Club
{
    public class UpdateClubRequest
    {
        public Address? Address { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Visibility { get; set; }
    }
}
