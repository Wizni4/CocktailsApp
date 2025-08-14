/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Common;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.StockAggregate
{
    public sealed class StockTransaction : Entity
    {
        public DateTime Date { get; }
        public string Description { get; }
        public decimal Quantity { get; }
        public StockTransactionType TransactionType { get; }

        internal StockTransaction(
            decimal quantity,
            string description,
            StockTransactionType transactionType,
            Guid createdBy) : base(createdBy)
        {
            if (!Enum.IsDefined(transactionType))
                throw new ArgumentException("Transaction type is invalid.");

            Quantity = quantity;
            Description = description;
            Date = DateTime.UtcNow;
            TransactionType = transactionType;
        }
    }
}
