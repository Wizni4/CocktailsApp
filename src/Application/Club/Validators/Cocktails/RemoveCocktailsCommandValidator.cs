/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */
using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;
using CocktailsApp.Application.SeedWork;

using FluentValidation;
using CocktailsApp.Domain.ClubAggregate;

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
        public RemoveCocktailsCommandValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(c => c.ClubId)
                .ValidGuid()
                .IsClubExists(unitOfWork.Set<DomainClub>());
            RuleFor(c => c.CocktailIds).ValidList();
            RuleForEach(c => c.CocktailIds).ValidGuid();
            RuleFor(c => c.ActorId).ValidGuid();

            RuleFor(c => c)
                .CustomAsync(async (command, validationContext, cancellationToken) =>
                {
                    var club = await unitOfWork.Set<DomainClub>().ReadAsync(
                        new ClubByIdSpecification(command.ClubId),
                        opt => opt.Include(c => c.Cocktails));

                    if (club != null)
                    {
                        // List missing cocktails
                        var missingCocktailIds = command.CocktailIds
                            .Where(id => !club.Cocktails.Any(cc => new ClubCocktailByIdSpecification(id).SpecExpression.Compile()(cc)))
                            .ToList();

                        // Add validation message if some cocktails dont exist in the club
                        if (missingCocktailIds.Any())
                            validationContext.AddFailure(
                                 $"The following cocktail do not exist in the club:\n- {string.Join("\n- ", missingCocktailIds)}");
                    }
                });
        }
    }
}
