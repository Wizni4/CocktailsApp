

using CocktailsApp.Domain.Clubs;
using CocktailsApp.ReadStore.Context;
using CocktailsApp.ReadStore.Projections;

namespace CocktailsApp.ReadStore.Clubs
{
    public sealed class ClubRoleCreatedProjection(
        EFReadDbContext dbContext
    ) : IProjectionHandler<ClubRoleCreatedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;
        public async Task HandleAsync(ClubRoleCreatedEvent @event, CancellationToken cancellationToken)
        {
            _dbContext.Add(new ClubRoleRead()
            {
                ClubId = @event.ClubId,
                RoleId = @event.RoleId,
                Name = @event.Name,
                IsOwnerRole = @event.IsOwnerRole,
            });

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
