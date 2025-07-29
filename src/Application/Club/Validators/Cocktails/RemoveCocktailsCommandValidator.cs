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

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Validates the <see cref="RemoveCocktailsCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class RemoveCocktailsCommandValidator : AbstractValidator<RemoveCocktailsCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveCocktailsCommandValidator"/> class.
        /// Defines validation rules for the <see cref="RemoveCocktailsCommand"/>.
        /// </summary>
        public RemoveCocktailsCommandValidator()
        {
            RuleFor(c => c.ClubId)
               .NotEqual(Guid.Empty)
               .WithMessage("ClubId must be a valid non-empty GUID.");

            RuleFor(c => c.CocktailIds)
                .NotNull()
                .WithMessage("CocktailIds must not be null.")
                .NotEmpty()
                .WithMessage("CocktailIds must not be an empty list.");

            RuleForEach(c => c.CocktailIds)
                 .NotEqual(Guid.Empty)
                 .WithMessage("CocktailId must be a valid non-empty GUID.");

            RuleFor(c => c.ActorId)
                .NotEqual(Guid.Empty)
                .WithMessage("ActorId must be a valid non-empty GUID.");
        }
    }
}
