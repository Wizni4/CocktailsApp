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
    public class UnitOfWork(EFDbContext dbContext, IServiceProvider serviceProvider) : IUnitOfWork
    {
        private readonly EFDbContext _dbContext = dbContext;
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public IRepository<T> Set<T>() where T : Entity, IAggregateRoot
        {
            return _serviceProvider.GetRequiredService<IRepository<T>>();
        }
    }
}
