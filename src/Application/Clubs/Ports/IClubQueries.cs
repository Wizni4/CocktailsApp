namespace CocktailsApp.Application.Clubs
{
    public interface IClubQueries
    {
        Task<ClubDetails?> GetClubDetailsAsync(Guid clubId, CancellationToken cancellationToken);
        Task<ClubListItem?> GetClubListItemAsync(Guid clubId, CancellationToken cancellationToken);
        Task<ClubMenu?> GetClubMenuAsync(Guid clubId, CancellationToken cancellationToken);
        Task<UserClubs?> GetUserClubsAsync(Guid userId, CancellationToken cancellationToken);
    }
}
