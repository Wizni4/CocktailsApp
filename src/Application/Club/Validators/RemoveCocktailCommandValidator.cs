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
    /// Validates the <see cref="RemoveCocktailCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class RemoveCocktailCommandValidator : AbstractValidator<RemoveCocktailCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveCocktailCommandValidator"/> class.
        /// Defines validation rules for the <see cref="RemoveCocktailCommand"/>.
        /// </summary>
        public RemoveCocktailCommandValidator()
        {
            RuleFor(c => c.ClubId)
               .NotEqual(Guid.Empty)
               .WithMessage("ClubId must be a valid non-empty GUID.");

            RuleFor(c => c.CocktailId)
                .NotEqual(Guid.Empty)
               .WithMessage("CocktailId must be a valid non-empty GUID.");

            RuleFor(c => c.ActorId)
                .NotEqual(Guid.Empty)
                .WithMessage("ActorId must be a valid non-empty GUID.");
        }
    }
}
