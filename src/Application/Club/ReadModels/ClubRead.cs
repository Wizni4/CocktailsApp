// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.Application.SeedWork;


namespace CocktailsApp.Application.Club
{
    public class ClubRead : ReadEntity
    {
        public Guid Id { get; set; }
        public ClubAddressRead Address { get; set; } = default!;
        public List<ClubCocktailRead> Cocktails { get; set; } = [];
        public List<ClubMemberRead> Members { get; set; } = [];
        public List<ClubRoleRead> Roles { get; set; } = [];
        public string Description { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Visibility { get; set; } = default!;
        public string? ImageId { get; set; } = default!;
    }
}
