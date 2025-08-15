
using CocktailsApp.Domain.Clubs;
using CocktailsApp.ReadStore.Persistence;
using CocktailsApp.ReadStore.Projections;


namespace CocktailsApp.ReadStore.Clubs
{
    public sealed class ClubCreatedProjection(
        EFReadDbContext dbContext
    ) : IProjectionHandler<ClubCreatedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;
        public async Task HandleAsync(ClubCreatedEvent @event, CancellationToken cancellationToken)
        {
            _dbContext.Add(new ClubRead
            {
                ClubId = @event.ClubId,
                Name = @event.Name,
                Description = @event.Description,
                Visibility = @event.Visibility.ToString(),
                Street = @event.Address?.Street,
                StreetNumber = @event.Address?.StreetNumber,
                City = @event.Address?.City,
                PostalCode = @event.Address?.PostalCode,
                State = @event.Address?.State,
                Country = @event.Address?.Country,
                ImageId = @event.ImageId,
            });


            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
