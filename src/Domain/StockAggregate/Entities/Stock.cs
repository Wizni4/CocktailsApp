/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.Shared;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.StockAggregate
{
    public class Stock : Entity, IAggregateRoot
    {
        public Ingredient Ingredient { get; }
        public decimal Quantity { get { return _stockTransactions.Sum(st => st.Quantity * (int)st.TransactionType); } }
        private readonly List<StockTransaction> _stockTransactions = [];
        public IReadOnlyCollection<StockTransaction> StockTransactions { get { return _stockTransactions.AsReadOnly(); } }
        public string Unit { get; }

        internal Stock(Ingredient ingredient, string unit)
        {
            Ingredient = ingredient;
            Unit = unit;
        }

        public void AddTransaction(decimal quantity, string description, StockTransactionType transactionType)
        {
            if (transactionType == StockTransactionType.Debit && Quantity < quantity)
                throw new ArgumentException("Quantity debit exceed remaining quantity", nameof(quantity));

            var transaction = new StockTransaction(quantity, description, transactionType);
            _stockTransactions.Add(transaction);

            // Check if stock quantity is now zero and raise an event if true
            if (Quantity == 0)
                DomainEvents.Raise(new IngredientOutOfStock(Id, Ingredient));
        }
    }
}
