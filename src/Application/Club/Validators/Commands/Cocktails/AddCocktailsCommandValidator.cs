/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */
using CocktailsApp.Application.Cocktail;
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;

using FluentValidation;

using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;
using DomainCocktail = CocktailsApp.Domain.CocktailAggregate.Cocktail;

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Validates the <see cref="AddCocktailsCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class AddCocktailsCommandValidator : ClubCommandValidator<AddCocktailsCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddCocktailsCommandValidator"/> class.
        /// Defines validation rules for the <see cref="AddCocktailsCommand"/> properties.
        /// </summary>
        public AddCocktailsCommandValidator(
            IUnitOfWork unitOfWork,
            IClubRepository clubRepository
        ) : base(clubRepository, [ClubPermissionType.AddCocktail])
        {
            RuleFor(c => c.CocktailIds).ValidList();
            RuleForEach(c => c.CocktailIds)
                .ValidGuid()
                .IsCocktailExists(unitOfWork.Set<DomainCocktail>());
        }
    }
}
