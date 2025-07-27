// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Cocktails.API.Models.Shared;

using CocktailsApp.Application.Club;
using CocktailsApp.Application.Cocktail;
using CocktailsApp.Application.Shared;
using CocktailsApp.Domain.ClubAggregate;

namespace API.Models.Club.Responses
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

    public class AddressResponse
    {
        /// <summary>
        /// Gets or sets the name of the street (e.g., "Main Street").
        /// </summary>
        public required string Street { get; set; }

        /// <summary>
        /// Gets or sets the street number (e.g., "42B").
        /// </summary>
        public required string StreetNumber { get; set; }

        /// <summary>
        /// Gets or sets the city name (e.g., "Paris").
        /// </summary>
        public required string City { get; set; }

        /// <summary>
        /// Gets or sets the postal or ZIP code (e.g., "75001").
        /// </summary>
        public required string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the state, province, or region (e.g., "Île-de-France").
        /// </summary>
        public required string State { get; set; }

        /// <summary>
        /// Gets or sets the country name (e.g., "France").
        /// </summary>
        public required string Country { get; set; }
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
        public List<ClubPermission> Permissions { get; set; } = [];

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
        public List<ClubPermission> Permissions { get; set; } = [];
    }

}
