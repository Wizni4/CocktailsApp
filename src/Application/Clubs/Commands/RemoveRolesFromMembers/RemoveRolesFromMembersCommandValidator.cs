

using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Clubs
{
    /// <summary>
    /// Validates the <see cref="RemoveRolesFromMembersCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public sealed class RemoveRolesFromMembersCommandValidator : ClubCommandValidator<RemoveRolesFromMembersCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveRolesFromMembersCommandValidator"/> class.
        /// Defines validation rules for the <see cref="RemoveRolesFromMembersCommand"/>.
        /// </summary>
        public RemoveRolesFromMembersCommandValidator()
        {
            RuleFor(c => c.Members).ValidList();
            RuleForEach(c => c.Members).SetValidator(new MemberRolesUpdateModelValidator());
        }
    }
}
