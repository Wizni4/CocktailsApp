// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.Shared;

using Microsoft.Extensions.Options;

using DomainCocktail = CocktailsApp.Domain.CocktailAggregate.Cocktail;

namespace CocktailsApp.Application.Cocktail
{
    public class CocktailSearchService(
        IMapper autoMapper,
        IUnitOfWork unitOfWork,
        IOptions<SearchSettingsDTO> option
    ) : SearchService<CocktailDTO, SearchCocktailQuery>(option), ICocktailsSearchService
    {
        private readonly IMapper _autoMapper = autoMapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public override async Task<IEnumerable<CocktailDTO>> GetSearchResultsAsync(SearchCocktailQuery query)
        {
            var searchLimit = _options.Value.Limit;

            var cocktails = await _unitOfWork.Set<DomainCocktail>().ReadRangeAsync(
                new CocktailByTermSpecification(query.Term), limit: searchLimit);

            return _autoMapper.Map<IEnumerable<CocktailDTO>>(cocktails);
        }
    }
}
