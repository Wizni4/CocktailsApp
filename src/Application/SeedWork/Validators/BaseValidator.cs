/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */
using CocktailsApp.Domain.SeedWork;

using FluentValidation;

using MediatR;

namespace CocktailsApp.Application.SeedWork
{
    public abstract class BaseValidator<TRequest> : AbstractValidator<TRequest> where TRequest : IBaseRequest
    {
        public void AssertMissingEntities<TEntity>(ValidationContext<TRequest> context, IEnumerable<TEntity> entities, IEnumerable<Guid> ids) where TEntity : Entity
        {
            var missingEntityIds = ids.Where(id => !entities
                .Any(e => new ByIdSpecification<TEntity>(id).SpecExpression.Compile()(e)));

            // Add validation message if some cocktails dont exist in the club
            if (missingEntityIds.Any())
                context.AddFailure(
                     $"The following {typeof(TEntity).Name} do not exist in the club:\n- {string.Join("\n- ", missingEntityIds)}");
        }
    }
}
