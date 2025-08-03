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
using DomainUser = CocktailsApp.Domain.UserAggregate.User;
using CocktailsApp.Application.User;

using FluentValidation;

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Validator for the <see cref="CreateClubCommand"/>.
    /// </summary>
    /// <remarks>
    /// Ensures that all required fields for creating a club are properly populated and valid.
    /// </remarks>
    public class CreateClubCommandValidator : AbstractValidator<CreateClubCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateClubCommandValidator"/> class.
        /// Defines validation rules for the <see cref="CreateClubCommand"/>.
        /// </summary>
        public CreateClubCommandValidator(IUnitOfWork unitOfWork, IClubService clubService)
        {
            RuleFor(c => c.Address).ValidAddress();
            RuleFor(c => c.Description).ValidString();
            RuleFor(c => c.Name).ValidString();
            RuleFor(c => c.OwnerId)
                .ValidGuid()
                .IsUserExists(unitOfWork.Set<DomainUser>());
            RuleFor(c => c.Visibility).ValidEnum();
            RuleFor(c => c)
                .CustomAsync(async (command, context, _) =>
                {
                    var clubLimit = await clubService.GetClubLimitInfoAsync(command.OwnerId);

                    if (!clubLimit.CanCreateClub)
                        context.AddFailure($"User {command.OwnerId} has reached the maximum number of owned club.\nOwned club(s): {clubLimit.NumberOfOwnedClubs}/{clubLimit.MaxNumberOfOwnedClubs}");
                });
        }
    }
}
