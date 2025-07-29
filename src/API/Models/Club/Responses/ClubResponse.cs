// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


using CocktailsApp.API.Models.Shared;
using CocktailsApp.Domain.ClubAggregate;
using System.ComponentModel.DataAnnotations;

namespace CocktailsApp.API.Models.Club
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
        /// Gets or sets the owner of the club.
        /// </summary>
        public required ClubMemberResponse Owner { get; set; }

        /// <summary>
        /// Gets or sets the list of roles available within the club.
        /// </summary>
        public List<ClubRoleResponse> Roles { get; set; } = [];

        /// <summary>
        /// Gets or sets the visibility level of the club (e.g., Public, Private).
        /// </summary>
        public int Visibility { get; set; }
    }

    public class ClubCocktailResponse
    {
        /// <summary>
        /// Gets the unique identifier of the cocktail.
        /// </summary>
        public Guid CocktailId { get; }
    }

    public class ClubMemberResponse
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the list of permissions granted to the club member.
        /// </summary>
        public List<string> Permissions { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of roles assigned to the club member.
        /// </summary>
        public List<ClubRoleResponse> Roles { get; set; } = [];
    }

    public class ClubRoleResponse
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the name of the role (e.g., "Administrator", "Moderator").
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of permissions associated with this role.
        /// </summary>
        public List<string> Permissions { get; set; } = [];
    }

}
