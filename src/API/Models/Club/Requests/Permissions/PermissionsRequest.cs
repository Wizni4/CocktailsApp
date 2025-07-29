// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Domain.ClubAggregate;

using System.ComponentModel.DataAnnotations;

namespace CocktailsApp.API.Models.Club
{
    public class PermissionsRequest
    {
        //[EnumDataType(typeof(ClubPermissionType))]
        public required IEnumerable<string> Permissions { get; set; }
    }
}
