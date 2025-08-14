using CocktailsApp.Domain.Clubs;


namespace CocktailsApp.Application.Clubs
{
    public interface IClubAccess
    {
        Task<bool> CanCreateClub(Guid? userId, CancellationToken cancellationToken);
        Task<bool> CanViewClub(Guid clubId, Guid? userId, CancellationToken cancellationToken);
        Task<bool> IsMember(Guid clubId, Guid userId, CancellationToken cancellationToken);
        Task<bool> IsOwner(Guid clubId, Guid userId, CancellationToken cancellationToken);
        Task<bool> HasPermission(Guid clubId, Guid userId, IEnumerable<PermissionType> permissions, CancellationToken cancellationToken);
    }
}
