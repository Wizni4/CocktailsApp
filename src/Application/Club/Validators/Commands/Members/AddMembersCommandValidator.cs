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
using CocktailsApp.Domain.ClubAggregate;

using FluentValidation;

using DomainUser = CocktailsApp.Domain.UserAggregate.User;

namespace CocktailsApp.Application.Club
{
    public class AddMembersCommandValidator : ClubCommandValidator<AddMembersCommand>
    {
        public AddMembersCommandValidator(
            IUnitOfWork unitOfWork,
            IClubRepository clubRepository
        ) : base(clubRepository, [ClubPermissionType.AddMember, ClubPermissionType.AddRoleToMember])
        {
            RuleFor(c => c.NewMembers).ValidList();
            RuleForEach(c => c.NewMembers)
                .ChildRules(a =>
                {
                    a.RuleFor(m => m.UserId).ValidGuid()
                        .IsUserExists(unitOfWork.Set<DomainUser>());
                    a.When(m => m.RoleIds is not null, () =>
                    {
                        a.RuleForEach(m => m.RoleIds).ValidGuid();
                    });
                });
            RuleFor(c => c)
                .CustomAsync(async (command, context, _) =>
                {
                    var club = await clubRepository.GetClubBydIdAsync(
                        command.ClubId,
                        opt => opt.Include(c => c.Cocktails));

                    if (club != null)
                        AssertMissingEntities(
                            context,
                            club.Roles,
                            command.NewMembers
                                .Where(m => m?.RoleIds != null)
                                .SelectMany(m => m!.RoleIds!));

                });
        }
    }
}
