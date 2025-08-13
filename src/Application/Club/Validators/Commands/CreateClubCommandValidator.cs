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
using CocktailsApp.Application.User;

using FluentValidation;

using DomainUser = CocktailsApp.Domain.UserAggregate.User;

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Validator for the <see cref="CreateClubCommand"/>.
    /// </summary>
    /// <remarks>
    /// Ensures that all required fields for creating a club are properly populated and valid.
    /// </remarks>
    public class CreateClubCommandValidator : CommandValidator<CreateClubCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateClubCommandValidator"/> class.
        /// Defines validation rules for the <see cref="CreateClubCommand"/>.
        /// </summary>
        public CreateClubCommandValidator()
        {
            RuleFor(c => c.Address).ValidAddress();
            RuleFor(c => c.Description).ValidString();
            RuleFor(c => c.Name).ValidString();
            RuleFor(c => c.OwnerId).ValidGuid();
            RuleFor(c => c.Visibility).ValidEnum();
        }
    }
}
