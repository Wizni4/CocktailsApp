using MediatR;


namespace CocktailsApp.Application.Common.Behaviors
{
    public sealed class IdempotencyBehavior<TRequest, TResponse>(
        IIdempotencyService store
    )
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IIdempotentCommand
    {
        private readonly IIdempotencyService _store = store;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var (found, value) = await _store.TryGetAsync<TResponse>(
                request.IdempotencyKey,
                cancellationToken);

            if (found && value is not null)
                return value;

            var response = await next();
            await _store.SaveAsync(
                request.IdempotencyKey,
                response!,
                cancellationToken);

            return response;
        }
    }

}
