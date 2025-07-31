// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


namespace CocktailsApp.API.Club
{
    public class ClubMemberResponse
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the list of roles assigned to the club member.
        /// </summary>
        public List<ClubRoleResponse> Roles { get; set; } = [];
    }
}
