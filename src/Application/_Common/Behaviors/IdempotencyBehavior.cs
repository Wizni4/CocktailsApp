using MediatR;


namespace CocktailsApp.Application.Common
{
    public sealed class IdempotencyBehavior<TRequest, TResponse>(
        IIdempotencyService store,
        IIdempotencyKeyService idempotencyKey
    )
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IIdempotentCommand
    {
        private readonly IIdempotencyService _store = store;
        private readonly IIdempotencyKeyService _idempotencyKey = idempotencyKey;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var key = _idempotencyKey.Value ?? throw new ArgumentException("Idempotency key is missing");

            var cached = await _store.TryGetAsync<TResponse>(key, cancellationToken);

            if (cached.HasValue)
                return cached.Value!;

            var response = await next();
            await _store.SaveAsync(
                key,
                response!,
                cancellationToken);

            return response;
        }
    }

}
