
using CocktailsApp.Application.Clubs;
using CocktailsApp.Domain.Clubs;
using CocktailsApp.ReadStore.Context;
using CocktailsApp.ReadStore.Projections;
using CocktailsApp.ReadStore.Users;

using Microsoft.EntityFrameworkCore;

namespace CocktailsApp.ReadStore.Clubs
{
    public sealed class ClubMemberRoleRemovedProjection(
        EFReadDbContext dbContext    
    ) : IProjectionHandler<ClubMemberRoleRemovedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;

        public async Task HandleAsync(ClubMemberRoleRemovedEvent @event, CancellationToken cancellationToken)
        {
            var clubMemberRole = await _dbContext.Set<ClubMemberRoleRead>()
                .FirstOrDefaultAsync(c =>
                    c.ClubId == @event.ClubId && 
                    c.ClubMemberId == @event.ClubMemberId &&
                    c.RoleId == @event.RoleId, 
                    cancellationToken);
            if (clubMemberRole != null) _dbContext.Remove(clubMemberRole);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
