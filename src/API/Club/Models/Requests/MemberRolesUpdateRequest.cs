// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace CocktailsApp.API.Club
{
    public class MemberRolesUpdateRequest
    {
        public required Guid Id { get; set; }
        public required IEnumerable<Guid> RoleIds { get; set; }
    }
}
