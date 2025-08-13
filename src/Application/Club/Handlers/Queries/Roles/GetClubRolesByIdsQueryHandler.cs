// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using AutoMapper;

using CocktailsApp.Application.SeedWork;

using System.Collections.ObjectModel;


namespace CocktailsApp.Application.Club
{
    internal class GetClubRolesByIdsQueryHandler(
        IClubRoleReader clubRoleReader,
        IMapper autoMapper
    ) : IQueryHandler<GetClubRolesByIdsQuery, ReadOnlyCollection<ClubRoleDTO>>
    {
        private readonly IClubRoleReader _clubRoleReader = clubRoleReader;
        private readonly IMapper _autoMapper = autoMapper;
        public async Task<ReadOnlyCollection<ClubRoleDTO>> Handle(GetClubRolesByIdsQuery request, CancellationToken cancellationToken)
        {
            return (await _clubRoleReader.ListAsync(
                new ClubRolesByIdsQuerySpecification(request.ClubId, request.RoleIds, _autoMapper),
                cancellationToken)).ToList().AsReadOnly();
        }
    }
}
