// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.SeedWork;

using System.Collections.ObjectModel;


namespace CocktailsApp.Application.Club
{
    internal class GetClubRolesQueryHandlers(
        IClubRoleReader clubRoleReader,
        IMapper autoMapper
    ) : IQueryHandler<GetClubRolesQuery, ReadOnlyCollection<ClubRoleDTO>>
    {
        private readonly IClubRoleReader _clubRoleReader = clubRoleReader;
        private readonly IMapper _autoMapper = autoMapper;
        public async Task<ReadOnlyCollection<ClubRoleDTO>> Handle(GetClubRolesQuery request, CancellationToken cancellationToken)
        {
            return (await _clubRoleReader.ListAsync(
                new ClubRolesQuerySpecification(request.ClubId, _autoMapper),
                cancellationToken)).ToList().AsReadOnly();
        }
    }
}

