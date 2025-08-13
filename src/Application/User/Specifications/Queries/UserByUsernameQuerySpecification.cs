// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using AutoMapper.QueryableExtensions;

using CocktailsApp.Application.SeedWork;


namespace CocktailsApp.Application.User
{
    public class UserByUsernameQuerySpecification(
        string userName,
        IMapper autoMapper
    ) : IQuerySpecification<UserRead, UserDTO>
    {
        private readonly string _userName = userName;
        private readonly IMapper _autoMapper = autoMapper;

        public IQueryable<UserRead> Filter(IQueryable<UserRead> query)
        {
            return query.Where(u => u.Username == _userName);
        }

        public IQueryable<UserDTO> Select(IQueryable<UserRead> filtered)
        {
            return filtered.ProjectTo<UserDTO>(_autoMapper.ConfigurationProvider);
        }
    }
}
