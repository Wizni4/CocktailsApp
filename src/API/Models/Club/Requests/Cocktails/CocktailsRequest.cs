// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Swashbuckle.AspNetCore.Annotations;

namespace CocktailsApp.API.Models.Club
{
    [SwaggerTag("Club - Cocktails")]
    public class CocktailsRequest
    {
        public required IEnumerable<Guid> CocktailIds { get; set; }
    }
}
