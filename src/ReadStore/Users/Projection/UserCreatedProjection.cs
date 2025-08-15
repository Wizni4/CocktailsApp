
using CocktailsApp.Domain.Users;
using CocktailsApp.ReadStore.Persistence;
using CocktailsApp.ReadStore.Projections;

namespace CocktailsApp.ReadStore.Users
{
    public sealed class UserCreatedProjection(
        EFReadDbContext dbContext
    ) : IProjectionHandler<UserCreatedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;

        public async Task HandleAsync(UserCreatedEvent @event, CancellationToken cancellationToken)
        {
            _dbContext.Add(new UserSummaryRead()
            {
                UserId = @event.UserId,
                Username = @event.UserName,
                ImageId = null
            });

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
