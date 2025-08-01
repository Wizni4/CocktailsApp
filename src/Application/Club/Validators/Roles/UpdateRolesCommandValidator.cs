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
    /// Validates the <see cref="UpdateRolesCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class UpdateRolesCommandValidator : ClubBaseValidator<UpdateRolesCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRolesCommandValidator"/> class.
        /// Defines validation rules for the <see cref="UpdateRolesCommand"/>.
        /// </summary>
        public UpdateRolesCommandValidator(IClubRepository clubRepository)
            : base(
                  clubRepository,
                  [
                      ClubPermissionType.ChangeRoleName,
                      ClubPermissionType.AddPermissionToRole,
                      ClubPermissionType.RemovePermissionFromRole
                  ]
            )
        {
            RuleFor(c => c.Roles).ValidList();
            RuleForEach(c => c.Roles)
                .ChildRules(a =>
                {
                    a.RuleFor(r => r.Id).ValidGuid();
                    a.When(r => r.Permissions is not null, () =>
                    {
                        a.RuleFor(r => r.Permissions!).ValidList();
                        a.RuleForEach(r => r.Permissions).ValidEnum();
                    });
                });

            RuleFor(c => c)
                .CustomAsync(async (command, context, _) =>
                {
                    var club = await clubRepository.GetClubBydIdAsync(
                        command.ClubId,
                        opt => opt.Include(c => c.Roles));

                    if (club != null)
                    {
                        AssertMissingEntities(
                            context,
                            club.Roles,
                            command.Roles.Select(r => r.Id));

                        // Get the list of found roles
                        var foundRoles = club.Roles
                            .Where(cr => command.Roles
                            .Any(r => new ClubRoleByIdSpecification(r.Id).SpecExpression.Compile()(cr)));

                        // Check if the roles already have the specified permissions
                        foreach (var clubRole in foundRoles)
                        {
                            var requestedPermissions = command.Roles
                                .FirstOrDefault(m => m.Id == clubRole.Id)?.Permissions;

                            var notExistingPermissions = (requestedPermissions ?? Enumerable.Empty<ClubPermissionType>())
                                .Where(p => !clubRole.Permissions.Any(rp => new ClubPermissionByTypeSpecification(p).SpecExpression.Compile()(rp)));

                            if (notExistingPermissions.Any())
                                context.AddFailure(
                                    $"The role: '{clubRole.Name}' do not have the following permissions:\n- {string.Join("\n- ", notExistingPermissions.ToString())}");

                            var alreadyExistingPermissions = clubRole.Permissions
                                .Where(rp => (requestedPermissions ?? Enumerable.Empty<ClubPermissionType>())
                                .Any(p => new ClubPermissionByTypeSpecification(p).SpecExpression.Compile()(rp)));

                            if (alreadyExistingPermissions.Any())
                                context.AddFailure(
                                    $"The role: '{clubRole.Name}' already has the following permissions:\n- {string.Join("\n- ", alreadyExistingPermissions.ToString())}");
                        }
                    }
                        
                });
        }
    }
}
