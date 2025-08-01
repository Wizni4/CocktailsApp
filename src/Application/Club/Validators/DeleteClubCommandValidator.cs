/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */
using CocktailsApp.Domain.ClubAggregate;

using FluentValidation;

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Validates the <see cref="DeleteClubCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class DeleteClubCommandValidator : ClubBaseValidator<DeleteClubCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteClubCommandValidator"/> class.
        /// Defines validation rules for the <see cref="DeleteClubCommand"/>.
        /// </summary>
        public DeleteClubCommandValidator(IClubRepository clubRepository)
            : base(clubRepository)
        {
        }
    }
}
