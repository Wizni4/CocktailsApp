// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


using CocktailsApp.Application.SeedWork;

namespace CocktailsApp.Application.Club
{
    public class ClubMemberRead : ReadEntity
    {
        public Guid ClubMemberId { get; set; }
        public Guid UserId { get; set; } = default!;
        public string Username { get; set; } = default!;
        public List<ClubRoleRead> Roles { get; set; } = [];
    }
}
