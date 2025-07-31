/*
 * Domain namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.SeedWork;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

/*
* Framework namespaces
*/

namespace CocktailsApp.Infrastructure.SeedWork
{
    public class UnitOfWork(EFDbContext dbContext, DomainEventDispatcher dispatcher, IServiceProvider serviceProvider) : IUnitOfWork
    {
        private readonly DomainEventDispatcher _dispatcher = dispatcher;
        private readonly EFDbContext _dbContext = dbContext;
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        public async Task SaveChangesAsync()
        {
            var domainEvents = _dbContext.ChangeTracker
                .Entries<AggregateRoot>()
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            foreach (var entity in _dbContext.ChangeTracker.Entries<AggregateRoot>())
                entity.Entity.ClearDomainEvents();


            // Update last update date of all modified entities
            UpdateTimestamps();
            await _dispatcher.DispatchAsync(domainEvents);
            await _dbContext.SaveChangesAsync();
        }

        public IRepository<T> Set<T>() where T : Entity, IAggregateRoot
        {
            return _serviceProvider.GetRequiredService<IRepository<T>>();
        }

        /// <summary>
        /// Update the <see cref="Entity.UpdateDate"> of all <see cref="Entity"/> modified or added
        /// </summary>
        private void UpdateTimestamps()
        {
            var modifiedEntries = _dbContext.ChangeTracker.Entries<Entity>()
                    .Where(e => e.State == EntityState.Modified || e.State == EntityState.Modified);

            foreach (var entry in modifiedEntries)
                entry.Entity.RefreshUpdateDate();
        }
    }
}
