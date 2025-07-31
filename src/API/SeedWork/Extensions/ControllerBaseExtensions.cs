// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Azure.Core;

using CocktailsApp.Domain.ClubAggregate;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace CocktailsApp.API.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static Guid GetUserId(this ControllerBase controller)
        {
            var userIdClaim = controller.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
               ?? controller.User.FindFirst("sub")?.Value
               ?? controller.User.FindFirst("userId")?.Value;

            if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
                throw new UnauthorizedAccessException("User not authorized");

            return userId;
        }

        public static string GetUsername(this ControllerBase controller)
        {
            var userName = controller.User.FindFirst("username")?.Value;

            if (userName is null)
                throw new UnauthorizedAccessException("User not authorized");

            return userName;
        }
    }
}
