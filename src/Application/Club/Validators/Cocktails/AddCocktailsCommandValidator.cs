/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */
using CocktailsApp.Application.SeedWork;
using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;
using DomainCocktail = CocktailsApp.Domain.CocktailAggregate.Cocktail;
using FluentValidation;
using CocktailsApp.Application.Cocktail;

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Validates the <see cref="AddCocktailsCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class AddCocktailsCommandValidator : AbstractValidator<AddCocktailsCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddCocktailsCommandValidator"/> class.
        /// Defines validation rules for the <see cref="AddCocktailsCommand"/> properties.
        /// </summary>
        public AddCocktailsCommandValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(c => c.ActorId).ValidGuid();
            RuleFor(c => c.ClubId)
                .ValidGuid()
                .IsClubExists(unitOfWork.Set<DomainClub>());
            RuleFor(c => c.CocktailIds).ValidList();
            RuleForEach(c => c.CocktailIds)
                .ValidGuid()
                .IsCocktailExists(unitOfWork.Set<DomainCocktail>());
        }
    }
}
