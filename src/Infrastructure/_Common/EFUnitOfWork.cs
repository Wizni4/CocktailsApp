using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Common;
using CocktailsApp.Infrastructure.Persistence;


namespace CocktailsApp.Infrastructure.Common
{
    public sealed class EFUnitOfWork(
        EFWriteDbContext dbContext
    ) : IUnitOfWork
    {
        private readonly EFWriteDbContext _dbContext = dbContext;

        public Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            var domainEvents = _dbContext.ChangeTracker
                .Entries<AggregateRoot>()
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            foreach (var entity in _dbContext.ChangeTracker.Entries<AggregateRoot>())
                entity.Entity.ClearDomainEvents();

            // Update last update date of all modified entities
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
