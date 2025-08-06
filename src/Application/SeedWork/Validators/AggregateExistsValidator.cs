

using CocktailsApp.Domain.SeedWork;

using FluentValidation;

namespace CocktailsApp.Application.SeedWork
{
    public class AggregateExistsValidator<TAggregateRoot> : AbstractValidator<Guid> where TAggregateRoot : AggregateRoot
    {
        public AggregateExistsValidator(IRepository<TAggregateRoot> repository)
        {
            RuleFor(id => id)
                .MustAsync(async (id, _) =>
                {
                    var aggregate = await repository.ReadAsync(new ByIdSpecification<TAggregateRoot>(id));
                    return aggregate is not null;
                }).WithMessage(id => $"{typeof(TAggregateRoot).Name} id: '{id}' was not found.");
        }
    }
}
