// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


using CocktailsApp.API.Shared;

namespace CocktailsApp.API.Club
{
    public class ClubResponse
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the physical address of the club.
        /// </summary>
        public Address? Address { get; set; }

        /// <summary>
        /// Gets or sets the list of cocktails associated with the club.
        /// </summary>
        public List<ClubCocktailResponse> Cocktails { get; set; } = [];

        /// <summary>
        /// Gets or sets the textual description of the club.
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Gets or sets the name of the club.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of members belonging to the club.
        /// </summary>
        public List<ClubMemberResponse> Members { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of roles available within the club.
        /// </summary>
        public List<ClubRoleResponse> Roles { get; set; } = [];

        /// <summary>
        /// Gets or sets the visibility level of the club (e.g., Public, Private).
        /// </summary>
        public required string Visibility { get; set; }
    }
}
