using MediatR;


namespace CocktailsApp.Application.Common
{
    public sealed class AuthorizationBehavior<TRequest, TResponse>(
        IEnumerable<IAuthorize<TRequest>> policies,
        ICurrentUser currentUser
    )
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseRequest
    {
        private readonly IEnumerable<IAuthorize<TRequest>> _policies = policies;
        private readonly ICurrentUser _currentUser = currentUser;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            foreach (var policy in _policies)
                await policy.AuthorizeAsync(request, _currentUser, cancellationToken);

            return await next();
        }
    }

}
