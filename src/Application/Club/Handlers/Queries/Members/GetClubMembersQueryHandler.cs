// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.SeedWork;

using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;


namespace CocktailsApp.Application.Club
{
    internal class GetClubMembersQueryHandler(
        IMapper autoMapper,
        IUnitOfWork unitOfWork
    ) : IQueryHandler<GetClubMembersQuery, IEnumerable<ClubMemberDTO>>
    {
        private readonly IMapper _autoMapper = autoMapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<ClubMemberDTO>> Handle(GetClubMembersQuery request, CancellationToken cancellationToken)
        {
            var club = await _unitOfWork.Set<DomainClub>().ReadAsync(
                new ClubByIdSpecification(request.ClubId),
                opt => opt.Include(c => c.Members)
                            .ThenInclude(m => m.Roles));

            return _autoMapper.Map<IEnumerable<ClubMemberDTO>>(club!.Members);
        }
    }
}
