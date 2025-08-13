// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.SeedWork;

using System.Collections.ObjectModel;


namespace CocktailsApp.Application.Club
{
    internal class GetClubMembersByIdsQueryHandler(
        IClubMemberReader clubMemberReader,
        IMapper autoMapper
    ) : IQueryHandler<GetClubMembersByIdsQuery, ReadOnlyCollection<ClubMemberDTO>>
    {
        private readonly IClubMemberReader _clubMemberReader = clubMemberReader;
        private readonly IMapper _autoMapper = autoMapper;
        public async Task<ReadOnlyCollection<ClubMemberDTO>> Handle(GetClubMembersByIdsQuery request, CancellationToken cancellationToken)
        {
            return (await _clubMemberReader.ListAsync(
                new ClubMembersByIdsQuerySpecification(request.ClubId, request.MemberIds, _autoMapper),
                cancellationToken)).ToList().AsReadOnly();
        }
    }
}
