/*
 * Domain namespaces
 */
using Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace Domain.StockAggregate
{
    public class StockTransaction : Entity
    {
        public DateTime Date { get; }
        public string Description { get; }
        public decimal Quantity { get; }
        public StockTransactionType TransactionType { get; }

        internal StockTransaction(decimal quantity, string description, StockTransactionType transactionType)
        {
            Quantity = quantity;
            Description = description;
            Date = DateTime.UtcNow;
            TransactionType = transactionType;
        }
    }
}
