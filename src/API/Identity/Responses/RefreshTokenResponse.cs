// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.API.Common;

namespace CocktailsApp.API.Identity
{
    public sealed record RefreshTokenResponse(
        string AccessToken,
        string? IdToken,
        string? RefreshToken
    ) : IResponse;
}
