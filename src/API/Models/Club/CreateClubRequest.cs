// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.Shared;
using CocktailsApp.Domain.ClubAggregate;

using Swashbuckle.AspNetCore.Annotations;

using System.ComponentModel.DataAnnotations;

namespace CocktailsApp.API.Models
{
    public class CreateClubRequest
    {
        [Required]
        public required Address Address { get; set; }

        [Required]
        [StringLength(200)]
        public required string Description { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [SwaggerParameter(Description = "Possible values: 0 (Private), 1 (Public)")]
        public int Visibility { get; set; }
    }

    public class Address
    {
        [Required]
        public required string Street { get; set; }
        [Required]
        public required string StreetNumber { get; set; }
        [Required]
        public required string City { get; set; }
        [Required]
        public required string PostalCode { get; set; }
        [Required]
        public required string State { get; set; }
        [Required]
        public required string Country { get; set; }
    }
}
