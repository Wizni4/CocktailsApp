// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using AutoMapper.QueryableExtensions;

using CocktailsApp.Application.SeedWork;


namespace CocktailsApp.Application.Cocktail
{
    public class SearchCocktailQuerySpecification(
        int limit,
        string term,
        IMapper autoMapper
    ) : IQuerySpecification<CocktailRead, CocktailDTO>
    {
        private readonly int _limit = limit;
        private readonly string _term = term;
        private readonly IMapper _autoMapper = autoMapper;

        public IQueryable<CocktailRead> Filter(IQueryable<CocktailRead> query)
        {
            return query.Where(c => c.Name.Contains(_term));
        }

        public IQueryable<CocktailDTO> Select(IQueryable<CocktailRead> filtered)
        {
            return filtered
                .ProjectTo<CocktailDTO>(_autoMapper.ConfigurationProvider)
                .Take(_limit);
        }
    }
}
