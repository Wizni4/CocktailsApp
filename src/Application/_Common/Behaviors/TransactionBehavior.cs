using MediatR;


namespace CocktailsApp.Application.Common
{
    public sealed class TransactionBehavior<TRequest, TResponse>(
        IUnitOfWork unitOfWork
    )
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var response = await next();
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return response;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
