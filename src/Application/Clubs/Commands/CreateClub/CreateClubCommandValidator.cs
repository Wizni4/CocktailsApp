using CocktailsApp.Application.Common;

using FluentValidation;

namespace CocktailsApp.Application.Clubs
{
    /// <summary>
    /// Validator for the <see cref="CreateClubCommand"/>.
    /// </summary>
    /// <remarks>
    /// Ensures that all required fields for creating a club are properly populated and valid.
    /// </remarks>
    public sealed class CreateClubCommandValidator : AbstractValidator<CreateClubCommand>
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
            When(c => c.Visibility is not null, () => 
            {
                RuleFor(c => c.Visibility).ValidEnum();
            });
        }
    }
}
