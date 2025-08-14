using CocktailsApp.Application.Common;

using Microsoft.Extensions.Options;

namespace CocktailsApp.Application.Ingredients
{
    public sealed class GetAllIngredientDetailsQueryHandler(
        IOptions<PageOptions> options,
        IIngredientQueries queries
    ) : IQueryHandler<GetAllIngredientDetailsQuery, PagedResult<IngredientDetails>>
    {
        private readonly IOptions<PageOptions> _options = options;
        private readonly IIngredientQueries _queries = queries;

        public Task<PagedResult<IngredientDetails>> Handle(GetAllIngredientDetailsQuery request, CancellationToken cancellationToken)
        {
            var cfg = _options.Value;
            var take = Math.Clamp(request.Limit ?? cfg.DefaultLimit, 1, cfg.MaxLimit);
            var skip = Math.Max(request.Offset ?? 0, 0);

            var criteria = new AllIngredientsCriteria(
                Take: take,
                Skip: skip
            );

            return _queries.GetAllIngredientDetailsAsync(criteria, cancellationToken);
        }
    }
}
