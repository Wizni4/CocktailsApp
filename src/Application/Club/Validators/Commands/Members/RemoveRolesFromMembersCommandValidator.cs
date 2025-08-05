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
    /// Validates the <see cref="RemoveRolesFromMembersCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class RemoveRolesFromMembersCommandValidator : ClubBaseValidator<RemoveRolesFromMembersCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveRolesFromMembersCommandValidator"/> class.
        /// Defines validation rules for the <see cref="RemoveRolesFromMembersCommand"/>.
        /// </summary>
        public RemoveRolesFromMembersCommandValidator(IClubRepository clubRepository)
            : base(clubRepository, [ClubPermissionType.RemoveRoleFromMember])
        {
            RuleFor(c => c.Members).ValidList();
            RuleForEach(c => c.Members).SetValidator(new MemberRolesUpdateModelValidator());
            RuleFor(c => c)
                .CustomAsync(async (command, context, _) =>
                {
                    var club = await clubRepository.GetClubBydIdAsync(
                        command.ClubId,
                        opt => opt.Include(c => c.Roles)
                                   .Include(c => c.Members)
                                    .ThenInclude(c => c.Roles));

                    if (club != null)
                    {
                        AssertMissingEntities(
                            context,
                            club.Members,
                            command.Members.Select(m => m.Id));

                        AssertMissingEntities(
                            context,
                            club.Roles,
                            command.Members.SelectMany(m => m.RoleIds));

                        // Get the list of found members
                        var foundMembers = club.Members
                            .Where(cm => command.Members
                            .Any(m => new ClubMemberByIdSpecification(m.Id).SpecExpression.Compile()(cm)));

                        // Check if the members doesnt have the specified roles
                        foreach (var clubMember in foundMembers)
                        {
                            var requestedRoles = command.Members
                                .FirstOrDefault(m => m.Id == clubMember.Id)?.RoleIds;

                            var notExistingRoles = (requestedRoles ?? Enumerable.Empty<Guid>())
                                .Where(id => !clubMember.Roles
                                .Any(r => new ClubRoleByIdSpecification(id).SpecExpression.Compile()(r)));

                            if (notExistingRoles.Any())
                                context.AddFailure(
                                    $"The member: '{clubMember.Id}' does not have the following roles:\n- {string.Join("\n- ", notExistingRoles)}");

                        }
                    }

                });
        }
    }
}
