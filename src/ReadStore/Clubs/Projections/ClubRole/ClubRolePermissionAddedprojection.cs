
using CocktailsApp.Domain.Clubs;
using CocktailsApp.ReadStore.Context;
using CocktailsApp.ReadStore.Projections;


namespace CocktailsApp.ReadStore.Clubs
{
    public sealed class ClubRolePermissionAddedprojection(
        EFReadDbContext dbContext    
    ) : IProjectionHandler<ClubRolePermissionAddedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;

        public async Task HandleAsync(ClubRolePermissionAddedEvent @event, CancellationToken cancellationToken)
        {
            _dbContext.Add(new ClubRolePermissionRead()
            {
                ClubId = @event.ClubId,
                RoleId = @event.RoleId,
                Permission = @event.Permission.ToString(),
            });

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
