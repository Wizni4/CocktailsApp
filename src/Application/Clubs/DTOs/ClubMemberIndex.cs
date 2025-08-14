using CocktailsApp.Application.Common;


namespace CocktailsApp.Application.Clubs
{
    public sealed record ClubMemberIndex(
        Guid ClubId,
        Guid MemberId,
        Guid UserId
    ) : IDTO;
}
