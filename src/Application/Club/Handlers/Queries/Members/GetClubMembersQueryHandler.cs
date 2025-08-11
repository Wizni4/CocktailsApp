// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.Application.SeedWork;


namespace CocktailsApp.Application.Club
{
    internal class GetClubMembersQueryHandler(
        IClubMemberQueries clubMemberQueries
    ) : IQueryHandler<GetClubMembersQuery, IEnumerable<ClubMemberDTO>>
    {
        private readonly IClubMemberQueries _clubMemberQueries = clubMemberQueries;

        public Task<IEnumerable<ClubMemberDTO>> Handle(GetClubMembersQuery request, CancellationToken cancellationToken)
        {
            return _clubMemberQueries.ReadRangeAsync(
                new ClubMemberQuerySpecification(request.ClubId));
        }
    }
}
