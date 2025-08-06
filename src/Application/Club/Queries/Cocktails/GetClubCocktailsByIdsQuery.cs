

namespace CocktailsApp.Application.Club
{
    public record GetClubCocktailsByIdsQuery(
        Guid ClubId,
        IEnumerable<Guid> CocktailIds
    ) : ClubQuery<IEnumerable<ClubCocktailDTO>>(ClubId);
}
