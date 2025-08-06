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
using CocktailsApp.Domain.ClubAggregate;

using FluentValidation;

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Validates the <see cref="RemoveMembersCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class RemoveMembersCommandValidator : ClubCommandValidator<RemoveMembersCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveMembersCommandValidator"/> class.
        /// Defines validation rules for the <see cref="RemoveMembersCommand"/>.
        /// </summary>
        public RemoveMembersCommandValidator(IClubRepository clubRepository)
            : base(clubRepository, [ClubPermissionType.RemoveMember])
        {
            RuleFor(c => c.MemberIds).ValidList();
            RuleForEach(c => c.MemberIds).ValidGuid();
            RuleFor(c => c)
                .CustomAsync(async (command, context, _) =>
                {
                    var club = await clubRepository.GetClubBydIdAsync(
                        command.ClubId,
                        opt => opt.Include(c => c.Members));

                    if (club != null)
                        AssertMissingEntities(
                            context,
                            club.Members,
                            command.MemberIds);
                });
        }
    }
}
