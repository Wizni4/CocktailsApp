

using CocktailsApp.Domain.Clubs;
using CocktailsApp.ReadStore.Persistence;
using CocktailsApp.ReadStore.Projections;

using Microsoft.EntityFrameworkCore;

namespace CocktailsApp.ReadStore.Clubs
{
    public sealed class ClubRolePermissionRemovedProjection(
        EFReadDbContext dbContext
    ) : IProjectionHandler<ClubRolePermissionRemovedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;

        public async Task HandleAsync(ClubRolePermissionRemovedEvent @event, CancellationToken cancellationToken)
        {
            var clubRolePermission = await _dbContext.Set<ClubRolePermissionRead>()
                .FirstOrDefaultAsync(crp =>
                    crp.ClubId == @event.ClubId &&
                    crp.RoleId == @event.RoleId &&
                    crp.Permission == @event.Permission.ToString(),
                    cancellationToken);
            if (clubRolePermission != null) _dbContext.Remove(clubRolePermission);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
