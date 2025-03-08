/*
 * Domain namespaces
 */
using Domain.CocktailAggregate;
using Domain.OrderAggregate;
using Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace Domain.StockAggregate
{
    public class StockService(IUnitOfWork unitOfWork) : IStockService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task DeductStockForOrder(ProcessedOrderEvent orderEvent)
        {
            var order = await _unitOfWork.Set<Order>().ReadAsync(new OrderByIdSpecification(orderEvent.OrderId), opt => opt.Include(o => o.Items));
            var cocktails = await _unitOfWork.Set<Cocktail>().ReadRangeAsync(new CocktailByIdsSpecification([.. order.Items.Select(i => i.CocktailId)]), opt => opt.Include(c => c.Ingredients));

            // Iterate over each cocktails and add a new StockTransaction for each cocktail ingredient
            foreach (var cocktail in cocktails)
            {
                cocktail.Ingredients.ToList().ForEach(async cocktailIngredient =>
                {
                    // Get the stock related to current ingredient
                    var stock = await _unitOfWork.Set<Stock>().ReadAsync(new StockByIngredientSpecification(cocktailIngredient.Ingredient), opt => opt.Include(s => s.StockTransactions));

                    // Add transaction
                    stock.AddTransaction(cocktailIngredient.Quantity, $"Debit caused by order:{order.Id}", StockTransactionType.Debit);

                    // Update stock in DB
                    _unitOfWork.Set<Stock>().Update(stock);
                });
            }

            // I'm not sure about that here
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
