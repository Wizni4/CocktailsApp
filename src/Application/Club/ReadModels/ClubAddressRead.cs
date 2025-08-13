// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


using CocktailsApp.Application.SeedWork;

namespace CocktailsApp.Application.Club
{
    public class ClubAddressRead : ReadEntity
    {
        public string Street { get; set; } = default!;
        public string StreetNumber { get; set; } = default!;
        public string City { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public string State { get; set; } = default!;
        public string Country { get; set; } = default!;
    }
}
