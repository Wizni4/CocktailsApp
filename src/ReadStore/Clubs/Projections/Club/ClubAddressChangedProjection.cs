using CocktailsApp.Domain.Clubs;
using CocktailsApp.ReadStore.Context;
using CocktailsApp.ReadStore.Projections;

using Microsoft.EntityFrameworkCore;

namespace CocktailsApp.ReadStore.Clubs
{
    public sealed class ClubAddressChangedProjection(
        EFReadDbContext dbContext    
    ) : IProjectionHandler<ClubAddressChangedEvent>
    {
        private readonly EFReadDbContext _dbContext= dbContext;
        public async Task HandleAsync(ClubAddressChangedEvent @event, CancellationToken cancellationToken)
        {
            var club = await _dbContext.Set<ClubRead>()
                .FirstOrDefaultAsync(c => c.ClubId == @event.ClubId, cancellationToken);

            if (club != null)
            {
                club.Street = @event.Address.Street;
                club.StreetNumber = @event.Address.StreetNumber;
                club.City = @event.Address.City;
                club.PostalCode = @event.Address.PostalCode;
                club.State = @event.Address.State;
                club.Country = @event.Address.Country;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
