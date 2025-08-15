

using CocktailsApp.Domain.Clubs;
using CocktailsApp.ReadStore.Persistence;
using CocktailsApp.ReadStore.Projections;

using Microsoft.EntityFrameworkCore;

using System.Data;

namespace CocktailsApp.ReadStore.Clubs
{
    public sealed class ClubRoleDeletedProjection(
        EFReadDbContext dbContext
    ) : IProjectionHandler<ClubRoleDeletedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;

        public async Task HandleAsync(ClubRoleDeletedEvent @event, CancellationToken cancellationToken)
        {
            var role = await _dbContext.Set<ClubRoleRead>()
                .FirstOrDefaultAsync(r => r.ClubId == @event.ClubId && r.RoleId == @event.RoleId, cancellationToken);
            if (role is not null) _dbContext.Remove(role);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
