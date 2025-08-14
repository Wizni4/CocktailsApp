using CocktailsApp.Application.Prices;
using CocktailsApp.Domain.Prices;
using CocktailsApp.Infrastructure.Common;
using CocktailsApp.Infrastructure.Persistence;


namespace CocktailsApp.Infrastructure.Prices
{
    public class PricingRepository(
        EFWriteDbContext dbContext,
        IIncludesService<Pricing> includesService
    ) : EFRepository<Pricing>(dbContext, includesService), IPricingRepository;
}
