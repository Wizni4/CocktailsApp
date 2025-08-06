// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using AutoMapper;

using CocktailsApp.Application.SeedWork;

using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;


namespace CocktailsApp.Application.Club
{
    internal class GetClubRolesByIdsQueryHandler(
        IMapper autoMapper,
        IUnitOfWork unitOfWork
    ) : IQueryHandler<GetClubRolesByIdsQuery, IEnumerable<ClubRoleDTO>>
    {
        private readonly IMapper _autoMapper = autoMapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<ClubRoleDTO>> Handle(GetClubRolesByIdsQuery request, CancellationToken cancellationToken)
        {
            var club = await _unitOfWork.Set<DomainClub>().ReadAsync(
                new ClubByIdSpecification(request.ClubId),
                opt => opt.Include(c => c.Roles));

            var clubRoles = club!.Roles.Where(r => request.RoleIds.Contains(r.Id));

            return _autoMapper.Map<IEnumerable<ClubRoleDTO>>(clubRoles);
        }
    }
}
