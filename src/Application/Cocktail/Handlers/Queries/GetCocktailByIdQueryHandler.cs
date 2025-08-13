// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using AutoMapper;

using CocktailsApp.Application.SeedWork;

namespace CocktailsApp.Application.Cocktail
{
    public class GetCocktailByIdQueryHandler(
        ICocktailReader cocktailReader,
        IMapper autoMapper
    ) : IQueryHandler<GetCocktailByIdQuery, CocktailDTO?>
    {
        private readonly ICocktailReader _cocktailReader = cocktailReader;
        private readonly IMapper _autoMapper = autoMapper;
        public Task<CocktailDTO?> Handle(GetCocktailByIdQuery request, CancellationToken cancellationToken)
        {
            return _cocktailReader.FirstOrDefaultAsync(
                new CocktailByIdQuerySpecification(request.CocktailId, _autoMapper),
                cancellationToken);
        }
    }
}
