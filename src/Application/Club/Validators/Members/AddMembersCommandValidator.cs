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
    public class AddMembersCommandValidator : AbstractValidator<AddMembersCommand>
    {
        public AddMembersCommandValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(c => c.ClubId)
                .ValidGuid()
                .IsClubExists(unitOfWork.Set<DomainClub>());
            RuleFor(c => c.NewMembers).ValidList();
            RuleForEach(c => c.NewMembers)
                .ChildRules(a =>
                {
                    a.RuleFor(m => m.UserId).ValidGuid();
                    a.When(m => m.RoleIds is not null, () =>
                    {
                        a.RuleForEach(m => m.RoleIds).ValidGuid();
                    });
                });
            RuleFor(c => c.ActorId).ValidGuid();
        }
    }
}
