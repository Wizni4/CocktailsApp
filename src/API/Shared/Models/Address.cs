// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations;

namespace CocktailsApp.API.Shared
{
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
