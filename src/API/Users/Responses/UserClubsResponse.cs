using CocktailsApp.API.Common;

namespace CocktailsApp.API.Users
{
    public sealed record UserClubsResponse(
        Guid UserId,
        IReadOnlyCollection<UserClubItemResponse> Clubs
    ) : IResponse;

    public sealed record UserClubItemResponse(
        Guid ClubId,
        string ClubName,
        bool IsOwner,
        string? ImageUrl,
        IReadOnlyCollection<string> RoleNames,
        IReadOnlyCollection<string> EffectivePermissions
    ) : IResponse;
}
