using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Common;
using CocktailsApp.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;


namespace CocktailsApp.Infrastructure.Common
{
    public sealed class EFUnitOfWork(
        EFWriteDbContext dbContext
    ) : IUnitOfWork, IAsyncDisposable
    {
        private readonly EFWriteDbContext _dbContext = dbContext;
        private IDbContextTransaction? _tx;

        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            // no-op if already in a transaction (supports nested handlers)
            if (_tx != null) return;
            _tx = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            // Always save first(this is where domain events / outbox rows get persisted)
            await _dbContext.SaveChangesAsync(cancellationToken);

            if (_tx != null)
            {
                await _tx.CommitAsync(cancellationToken);
                await _tx.DisposeAsync();
                _tx = null;
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            if (_tx != null)
            {
                await _tx.RollbackAsync(cancellationToken);
                await _tx.DisposeAsync();
                _tx = null;
            }

            // Throw away any tracked changes so subsequent requests start clean
            _dbContext.ChangeTracker.Clear(); // EF Core 6+
        }

        public async ValueTask DisposeAsync()
        {
            if (_tx != null)
            {
                await _tx.DisposeAsync();
                _tx = null;
            }
        }

    }
}
