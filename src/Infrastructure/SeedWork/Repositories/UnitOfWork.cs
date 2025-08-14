/*
 * Domain namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

/*
* Framework namespaces
*/

namespace CocktailsApp.Infrastructure.SeedWork
{
    public sealed class UnitOfWork(
        EFWriteDbContext dbContext
    ) : IUnitOfWork
    {
        private readonly EFWriteDbContext _dbContext = dbContext;
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            var domainEvents = _dbContext.ChangeTracker
                .Entries<AggregateRoot>()
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            // Persist outbox messages
            _dbContext.Set<OutboxMessage>().AddRange(
                domainEvents.Select(OutboxMessageFactory.FromDomainEvent));

            foreach (var entity in _dbContext.ChangeTracker.Entries<AggregateRoot>())
                entity.Entity.ClearDomainEvents();

            // Update last update date of all modified entities
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
