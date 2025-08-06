// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;

using CocktailsApp.Application.SeedWork;

using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;


namespace CocktailsApp.Application.Club
{
    public class GetClubCocktailsQueryHandler(
        IMapper autoMapper,
        IUnitOfWork unitOfWork
    ) : IQueryHandler<GetClubCocktailsQuery, IEnumerable<ClubCocktailDTO>>
    {
        private readonly IMapper _autoMapper = autoMapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<ClubCocktailDTO>> Handle(GetClubCocktailsQuery request, CancellationToken cancellationToken)
        {
            var club = await _unitOfWork.Set<DomainClub>().ReadAsync(
                new ClubByIdSpecification(request.ClubId),
                opt => opt.Include(c => c.Cocktails));

            return _autoMapper.Map<IEnumerable<ClubCocktailDTO>>(club!.Cocktails);
        }
    }
}
