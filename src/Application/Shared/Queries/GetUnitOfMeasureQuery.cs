using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Shared
{
    public sealed record GetUnitOfMeasureQuery : IQuery<IReadOnlyCollection<string>>;
}
