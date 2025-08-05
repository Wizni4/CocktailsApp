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
    public class CreateRolesCommandValidator : ClubBaseValidator<CreateRolesCommand>
    {
        public CreateRolesCommandValidator(IClubRepository clubRepository)
            : base(clubRepository, [ClubPermissionType.CreateRole, ClubPermissionType.AddPermissionToRole])
        {
            RuleFor(c => c.NewRoles).ValidList();
            RuleForEach(c => c.NewRoles)
                 .ChildRules(a =>
                 {
                     a.RuleFor(r => r.Name).ValidString();
                     a.When(r => r.Permissions is not null, () =>
                     {
                         a.RuleFor(m => m.Permissions!).ValidList();
                         a.RuleForEach(m => m.Permissions).ValidEnum();
                     });
                 });

            RuleFor(c => c)
                .CustomAsync(async (command, context, _) =>
                {
                    var club = await clubRepository.GetClubBydIdAsync(
                        command.ClubId,
                        opt => opt.Include(c => c.Roles));

                    if(club != null)
                    {
                        var alreadyExistingNames = command.NewRoles
                        .Select(c => c.Name)
                        .Where(name => club.Roles.Any(r => r.Name == name));

                        // Add validation message if some cocktails dont exist in the club
                        if (alreadyExistingNames.Any())
                            context.AddFailure(
                                 $"The following roles already exist in the club:\n- {string.Join("\n- ", alreadyExistingNames)}");
                    }
                });
        }
    }
}
