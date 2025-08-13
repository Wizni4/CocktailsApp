// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using AutoMapper.QueryableExtensions;

using CocktailsApp.Application.SeedWork;


namespace CocktailsApp.Application.User
{
    public class SearchUserQuerySpecification(
        int limit,
        string term,
        IMapper autoMapper
    ) : IQuerySpecification<UserRead, UserDTO>
    {
        private readonly int _limit = limit;
        private readonly string _term = term;
        private readonly IMapper _autoMapper = autoMapper;

        public IQueryable<UserRead> Filter(IQueryable<UserRead> query)
        {
            return query.Where(u => u.Username.Contains(_term));
        }

        public IQueryable<UserDTO> Select(IQueryable<UserRead> filtered)
        {
            return filtered
                .ProjectTo<UserDTO>(_autoMapper.ConfigurationProvider)
                .Take(_limit);
        }
    }
}
