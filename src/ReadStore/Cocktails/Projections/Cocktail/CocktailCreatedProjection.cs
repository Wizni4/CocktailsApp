
using CocktailsApp.Domain.Cocktails;
using CocktailsApp.ReadStore.Persistence;
using CocktailsApp.ReadStore.Projections;

namespace CocktailsApp.ReadStore.Cocktails
{
    public sealed class CocktailCreatedProjection(
        EFReadDbContext dbContext
    ) : IProjectionHandler<CocktailCreatedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;

        public async Task HandleAsync(CocktailCreatedEvent @event, CancellationToken cancellationToken)
        {
            _dbContext.Add(new CocktailRead()
            {
                CocktailId = @event.CocktailId,
                Name = @event.Name,
                Description = @event.Description,
                ImageId = @event.ImageId,
            });

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
