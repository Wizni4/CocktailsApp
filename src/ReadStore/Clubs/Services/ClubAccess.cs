

using CocktailsApp.Application.Clubs;
using CocktailsApp.Domain.Clubs;

namespace CocktailsApp.ReadStore.Clubs
{
    public sealed class ClubAccess : IClubAccess
    {
        public Task<bool> CanCreateClub(Guid? userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CanViewClub(Guid clubId, Guid? userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPermission(Guid clubId, Guid userId, IEnumerable<PermissionType> permissions, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsMember(Guid clubId, Guid userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsOwner(Guid clubId, Guid userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
