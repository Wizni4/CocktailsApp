/*
 * Domain namespaces
 */
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.UserAggregate;

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

            await _dispatcher.DispatchAsync(domainEvents);
            await _dbContext.SaveChangesAsync();
        }

        public IRepository<T> Set<T>() where T : Entity, IAggregateRoot
        {
            return _serviceProvider.GetRequiredService<IRepository<T>>();
        }
    }
}
