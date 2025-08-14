

using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Stocks;

namespace CocktailsApp.Application.Stocks
{
    public interface IStockRepository : IRepository<Stock>
    {
    }
}
