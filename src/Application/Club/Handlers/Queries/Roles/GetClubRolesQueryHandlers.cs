// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.Application.SeedWork;


namespace CocktailsApp.Application.Club
{
    internal class GetClubRolesQueryHandlers(
        IClubRoleQueries clubRoleQueries
    ) : IQueryHandler<GetClubRolesQuery, IEnumerable<ClubRoleDTO>>
    {
        private readonly IClubRoleQueries _clubRoleQueries = clubRoleQueries;

        public Task<IEnumerable<ClubRoleDTO>> Handle(GetClubRolesQuery request, CancellationToken cancellationToken)
        {
            return _clubRoleQueries.ReadRangeAsync(
                new ClubRolesQuerySpecification(request.ClubId));
        }
    }
}

