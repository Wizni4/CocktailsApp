// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.API.Common;


namespace CocktailsApp.API.Clubs
{
    public sealed record ClubDetailsResponse(
        Guid ClubId,
        string Name,
        string Description,
        string Visibility,
        string? Street,
        string? StreetNumber,
        string? City,
        string? PostalCode,
        string? State,
        string? Country,
        string? ImageUrl,
        IReadOnlyCollection<ClubRoleResponse> Roles,
        IReadOnlyCollection<ClubMemberResponse> Members,
        IReadOnlyCollection<ClubCocktailResponse> Cocktails
    ) : IResponse;

    public sealed record ClubRoleResponse(
        Guid RoleId,
        string Name,
        bool IsOwnerRole,
        IReadOnlyCollection<string> Permissions
    ) : IResponse;

    public sealed record ClubMemberResponse(
        Guid ClubMemberId,
        Guid UserId,
        string Username,
        List<string> Roles,
        bool IsOwner,
        string? ImageUrl
    ) : IResponse;

    public sealed record ClubCocktailResponse(
        Guid ClubCocktailId,
        Guid CocktailId,
        string CocktailName,
        string? ImageUrl
    ) : IResponse;
}
