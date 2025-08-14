
using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Common;


namespace CocktailsApp.Application.Shared
{
    public sealed class GetUnitOfMeasureQueryHandler
        : IQueryHandler<GetUnitOfMeasureQuery, IReadOnlyCollection<string>>
    {
        public Task<IReadOnlyCollection<string>> Handle(GetUnitOfMeasureQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<string> types = Enum
                .GetNames(typeof(UnitOfMeasure))
                .ToList()
                .AsReadOnly();

            return Task.FromResult(types);
        }
    }
}
