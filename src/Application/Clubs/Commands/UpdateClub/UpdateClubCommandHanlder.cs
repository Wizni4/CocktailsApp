using CocktailsApp.Application.Common;

using MediatR;

namespace CocktailsApp.Application.Clubs
{
    public sealed class UpdateClubCommandHanlder(
        ICurrentUser user,
        IClubRepository clubRepository
    ) : ICommandHandler<UpdateClubCommand, Unit>
    {
        private readonly ICurrentUser _user = user;
        private readonly IClubRepository _clubRepository = clubRepository;

        public async Task<Unit> Handle(UpdateClubCommand request, CancellationToken cancellationToken)
        {
            // Get the club from the db
            var club = await _clubRepository.ReadAsync(
                new LoadClubCommandSpecification(request.ClubId),
                cancellationToken);

            // Update Address
            if (request.Address != null)
                club!.UpdateAddress(
                    request.Address.Street,
                    request.Address.StreetNumber,
                    request.Address.City,
                    request.Address.PostalCode,
                    request.Address.State,
                    request.Address.Country,
                    _user.UserId);

            // Update Name
            if (request.Name != null)
                club!.UpdateName(request.Name, _user.UserId);

            // Update Description
            if (request.Description != null)
                club!.UpdateDescription(request.Description, _user.UserId);

            // Update Visibility
            if (request.Visibility != null)
                club!.UpdateVisibility(request.Visibility.Value, _user.UserId);

            // Update the club
            await _clubRepository.UpdateAsync(club!, cancellationToken);

            // Return club updated successfully
            return Unit.Value;
        }
    }
}
