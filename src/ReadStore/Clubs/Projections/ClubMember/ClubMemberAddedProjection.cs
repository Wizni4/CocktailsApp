

using CocktailsApp.Domain.Clubs;
using CocktailsApp.ReadStore.Context;
using CocktailsApp.ReadStore.Projections;
using CocktailsApp.ReadStore.Users;

using Microsoft.EntityFrameworkCore;

namespace CocktailsApp.ReadStore.Clubs
{
    public sealed class ClubMemberAddedProjection(
        EFReadDbContext dbContext
    ) : IProjectionHandler<ClubMemberAddedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;

        public async Task HandleAsync(ClubMemberAddedEvent @event, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Set<UserSummaryRead>()
                .FirstOrDefaultAsync(u => u.UserId == @event.UserId, cancellationToken);

            _dbContext.Add(new ClubMemberRead()
            {
                ClubId = @event.ClubId,
                ClubMemberId = @event.ClubMemberId,
                UserId = @event.UserId,
            });

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
