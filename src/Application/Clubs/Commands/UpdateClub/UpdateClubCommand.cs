
using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Clubs;

using MediatR;


namespace CocktailsApp.Application.Clubs
{
    public sealed record UpdateClubCommand(
        Guid ClubId,
        Common.Address? Address,
        string? Name,
        string? Description,
        Visibility? Visibility,
        Guid RequestId
    ) : ClubCommand<Unit>(ClubId), IIdempotentCommand
    {
        public string IdempotencyKey => $"UpdateClub:{RequestId}";
    }
}
