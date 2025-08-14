using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Clubs
{
    /// <summary>
    /// Validates the <see cref="RemoveMembersCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public sealed class RemoveMembersCommandValidator : ClubCommandValidator<RemoveMembersCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveMembersCommandValidator"/> class.
        /// Defines validation rules for the <see cref="RemoveMembersCommand"/>.
        /// </summary>
        public RemoveMembersCommandValidator()
        {
            RuleFor(c => c.MemberIds).ValidList();
            RuleForEach(c => c.MemberIds).ValidGuid();
        }
    }
}
