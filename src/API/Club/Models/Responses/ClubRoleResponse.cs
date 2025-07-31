// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


namespace CocktailsApp.API.Club
{
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
