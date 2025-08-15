
using CocktailsApp.Domain.Clubs;
using CocktailsApp.ReadStore.Persistence;
using CocktailsApp.ReadStore.Projections;

using Microsoft.EntityFrameworkCore;

namespace CocktailsApp.ReadStore.Clubs
{
    public sealed class ClubRoleRenamedProjection(
        EFReadDbContext dbContext
    ) : IProjectionHandler<ClubRoleRenamedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;

        public async Task HandleAsync(ClubRoleRenamedEvent @event, CancellationToken cancellationToken)
        {
            var clubRole = await _dbContext.Set<ClubRoleRead>()
                .FirstOrDefaultAsync(crp =>
                    crp.ClubId == @event.ClubId &&
                    crp.RoleId == @event.RoleId,
                    cancellationToken);
            if (clubRole != null) clubRole.Name = @event.Name;

            var clubMemberRoles = await _dbContext.Set<ClubMemberRoleRead>()
                .Where(cmr =>
                    cmr.ClubId == @event.ClubId &&
                    cmr.RoleId == @event.RoleId)
                .ToListAsync(cancellationToken);
            foreach (var clubMemberRole in clubMemberRoles)
                clubMemberRole.RoleName = @event.Name;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
