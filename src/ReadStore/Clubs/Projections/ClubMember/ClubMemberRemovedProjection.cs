
using CocktailsApp.Domain.Clubs;
using CocktailsApp.ReadStore.Context;
using CocktailsApp.ReadStore.Projections;

using Microsoft.EntityFrameworkCore;

namespace CocktailsApp.ReadStore.Clubs
{
    public sealed class ClubMemberRemovedProjection(
        EFReadDbContext dbContext    
    ) : IProjectionHandler<ClubMemberRemovedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;

        public async Task HandleAsync(ClubMemberRemovedEvent @event, CancellationToken cancellationToken)
        {
            var clubMember = await _dbContext.Set<ClubMemberRoleRead>()
                .FirstOrDefaultAsync(c => c.ClubId == @event.ClubId && c.ClubMemberId == @event.ClubMemberId, cancellationToken);
            if (clubMember != null) _dbContext.Remove(clubMember);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
