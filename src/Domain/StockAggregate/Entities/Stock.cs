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
    public sealed class Stock : AggregateRoot, IAggregateRoot
    {
        public Guid ClubId { get => _clubId; }
        private readonly Guid _clubId;
        public Guid IngredientId { get => _ingredientId; }
        private readonly Guid _ingredientId;
        public decimal Quantity { get => _stockTransactions.Sum(st => st.Quantity * (int)st.TransactionType); }
        private readonly List<StockTransaction> _stockTransactions = [];
        public IReadOnlyCollection<StockTransaction> StockTransactions { get => _stockTransactions.AsReadOnly(); }
        public UnitOfMeasure Unit { get => _unit; }
        private readonly UnitOfMeasure _unit;
        private Stock() { }
        internal Stock(Guid clubId, Guid ingredientId, UnitOfMeasure unit, Guid createdBy) : base(createdBy)
        {
            if (!Enum.IsDefined(unit))
                throw new ArgumentException("Unit is invalid.");

            _clubId = clubId;
            _ingredientId = ingredientId;
            _unit = unit;
        }

        public void AddTransaction(decimal quantity, string description, StockTransactionType transactionType, Guid createdBy)
        {
            if (transactionType == StockTransactionType.Debit && Quantity < quantity)
                throw new ArgumentException("Quantity debit exceed remaining quantity", nameof(quantity));

            var transaction = new StockTransaction(quantity, description, transactionType, createdBy);
            _stockTransactions.Add(transaction);
            Touch();

            // Check if stock quantity is now zero and raise an event if true
            if (Quantity == 0)
                AddDomainEvent(new IngredientOutOfStock(Id, IngredientId));
        }
    }
}
