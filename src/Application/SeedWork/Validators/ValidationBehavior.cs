/*
 * Domain namespaces
 */
/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */
using FluentValidation;
using MediatR;


namespace CocktailsApp.Application.SeedWork
{
    /// <summary>
    /// Pipeline behavior for performing validation on requests before they are handled.
    /// Uses FluentValidation validators to validate the request.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request being validated. Must be non-nullable.</typeparam>
    /// <typeparam name="TResponse">The type of response produced by the handler.</typeparam>
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

        /// <summary>
        /// Invokes the validation behavior on the request.
        /// Throws a <see cref="ValidationException"/> if validation fails.
        /// </summary>
        /// <param name="request">The request to validate.</param>
        /// <param name="next">The next delegate in the pipeline to invoke if validation succeeds.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The result of the next handler in the pipeline.</returns>
        /// <exception cref="ValidationException">Thrown if validation fails.</exception>
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
                throw new ValidationException(failures);

            return await next();
        }
    }
}
