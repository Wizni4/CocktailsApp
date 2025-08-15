

using CocktailsApp.Application.Clubs;
using CocktailsApp.ReadStore.Persistence;

using Microsoft.EntityFrameworkCore;

namespace CocktailsApp.ReadStore.Clubs
{
    public sealed class ClubQueries(
        EFReadDbContext dbContext
    ) : IClubQueries
    {
        private readonly EFReadDbContext _dbContext = dbContext;
        public Task<ClubDetails?> GetClubDetailsAsync(Guid clubId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ClubListItem?> GetClubListItemAsync(Guid clubId, CancellationToken cancellationToken)
        {
            var clubRead = await _dbContext.Set<ClubRead>()
                .FirstOrDefaultAsync(c => c.ClubId == clubId, cancellationToken);
            if (clubRead == null) return null;

            var clubMemberCount = await _dbContext.Set<ClubCocktailRead>().Where(c => c.ClubId == clubId).CountAsync(cancellationToken);
            var clubCocktailCount = await _dbContext.Set<ClubMemberRead>().Where(c => c.ClubId == clubId).CountAsync(cancellationToken);

            return new ClubListItem(
                ClubId: clubRead.ClubId,
                Name: clubRead.Name,
                Description: clubRead.Description,
                Visibility: clubRead.Visibility,
                City: clubRead.City,
                Country: clubRead.Country,
                ImageId: clubRead.ImageId,
                CocktailCount: clubCocktailCount,
                MemberCount: clubMemberCount
            );
        }

        public Task<ClubMenu?> GetClubMenuAsync(Guid clubId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<UserClubs?> GetUserClubsAsync(Guid userId, CancellationToken cancellationToken)
        {
            var clubMembers = await _dbContext.Set<ClubMemberRead>()
                .Where(cm => cm.UserId == userId)
                .ToListAsync(cancellationToken);
            var clubIds = clubMembers.Select(m => m.ClubId);
            var clubMemberIds = clubMembers.Select(m => m.ClubMemberId);

            var memberRoles = await _dbContext.Set<ClubMemberRoleRead>()
                .Where(mr => clubMemberIds.Any(mId => mId == mr.ClubMemberId))
                .ToListAsync(cancellationToken);
            var memberRoleIds = memberRoles.Select(mr => mr.RoleId);

            var rolePermissions = await _dbContext.Set<ClubRolePermissionRead>()
                .Where(rp => memberRoleIds.Any(rId => rId == rp.RoleId))
                .ToListAsync(cancellationToken);

            var clubs = await _dbContext.Set<ClubRead>()
                .Where(c => clubIds.Any(mId => mId == c.ClubId))
                .Select(c => new { c.ClubId, c.Name, c.Description, c.ImageId })
                .ToListAsync(cancellationToken);

            var items = clubs.Select(c =>
            {
                return new UserClubItem(
                    ClubId: c.ClubId,
                    ClubName: c.Name,
                    ClubDescription: c.Description,
                    IsOwner: memberRoles.Any(mr => mr.ClubId == c.ClubId && mr.IsOwnerRole),
                    ImageId: c.ImageId,
                    RoleNames: memberRoles.Where(mr => mr.ClubId == c.ClubId).Select(mr => mr.RoleName).ToList().AsReadOnly(),
                    EffectivePermissions: rolePermissions.Where(rp => rp.ClubId == c.ClubId).Select(rp => rp.Permission).ToList().AsReadOnly()
                );
            }).ToList();

            return new UserClubs(
                UserId: userId,
                Clubs: items.AsReadOnly()
            );
        }
    }
}
