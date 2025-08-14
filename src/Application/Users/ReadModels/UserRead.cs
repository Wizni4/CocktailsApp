// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Application.SeedWork;


namespace CocktailsApp.Application.Users
{
    public class UserRead : ReadEntity
    {
        public Guid Id { get; set; } = default;
        public string Username { get; set; } = default!;
        public string? ImageId { get; set; } = null;
    }
}
