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

using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Validates the <see cref="AddRolesToMembersCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class AddRolesToMembersCommandValidator : ClubBaseValidator<AddRolesToMembersCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddRolesToMembersCommandValidator"/> class.
        /// Defines validation rules for the <see cref="AddRolesToMembersCommand"/> properties.
        /// </summary>
        public AddRolesToMembersCommandValidator(
            IClubRepository clubRepository
        ) : base(clubRepository, [ClubPermissionType.AddRoleToMember])
        {
            RuleFor(c => c.Members).ValidList();
            RuleForEach(c => c.Members).SetValidator(new MemberRolesUpdateModelValidator());
            RuleFor(c => c)
                .CustomAsync(async (command, context, _) =>
                {
                    var club = await clubRepository.GetClubBydIdAsync(
                        command.ClubId,
                        opt => opt.Include(c => c.Cocktails)
                                  .Include(c => c.Roles)
                                  .Include(c => c.Members)
                                    .ThenInclude(c => c.Roles));

                    if (club != null)
                    {
                        // Check that roles exists in the club
                        AssertMissingEntities(
                            context,
                            club.Roles,
                            command.Members.SelectMany(m => m.RoleIds));

                        // Check that members exists in the club
                        AssertMissingEntities(
                            context,
                            club.Members,
                            command.Members.Select(m => m.Id));

                        // Get the list of found members
                        var foundMembers = club.Members
                            .Where(cm => command.Members
                            .Any(m => new ClubMemberByIdSpecification(m.Id).SpecExpression.Compile()(cm)));

                        // Check if the members already have the specified roles
                        foreach(var clubMember in foundMembers)
                        {
                            var requestedRoles = command.Members
                                .FirstOrDefault(m => m.Id == clubMember.Id)?.RoleIds;

                            var alreadyExistingRoles = clubMember.Roles
                                .Where(r => (requestedRoles ?? Enumerable.Empty<Guid>())
                                .Any(id => new ClubRoleByIdSpecification(id).SpecExpression.Compile()(r)));

                            if (alreadyExistingRoles.Any())
                                context.AddFailure(
                                    $"The member: '{clubMember.Id}' already has the following roles:\n- {string.Join("\n- ", alreadyExistingRoles)}");

                        }
                    }
                });
        }
    }
}
