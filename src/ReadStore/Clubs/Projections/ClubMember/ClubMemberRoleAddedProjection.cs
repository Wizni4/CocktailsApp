
using CocktailsApp.Domain.Clubs;
using CocktailsApp.ReadStore.Persistence;
using CocktailsApp.ReadStore.Projections;

using Microsoft.EntityFrameworkCore;

namespace CocktailsApp.ReadStore.Clubs
{
    public sealed class ClubMemberRoleAddedProjection(
        EFReadDbContext dbContext
    ) : IProjectionHandler<ClubMemberRoleAddedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;

        public async Task HandleAsync(ClubMemberRoleAddedEvent @event, CancellationToken cancellationToken)
        {
            var clubRole = await _dbContext.Set<ClubRoleRead>()
                .FirstOrDefaultAsync(c =>
                    c.ClubId == @event.ClubId &&
                    c.RoleId == @event.RoleId,
                    cancellationToken);

            _dbContext.Add(new ClubMemberRoleRead()
            {
                ClubId = @event.ClubId,
                ClubMemberId = @event.ClubMemberId,
                RoleId = @event.RoleId,
                RoleName = clubRole?.Name!,
                IsOwnerRole = clubRole?.IsOwnerRole ?? false,
            });

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
