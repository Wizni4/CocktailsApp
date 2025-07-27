/*
 * Domain namespaces
 */
using CocktailsApp.Domain.CocktailAggregate;
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.SeedWork;
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
/*
 * Framework namespaces
 */
using AutoMapper;

namespace CocktailsApp.Application.Club
{
    public class AddMemberCommandHandler(IUnitOfWork unitOfWork, IMapper autoMapper)
        : ICommandHandler<AddMemberComand, ClubDTO>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _autoMapper = autoMapper;

        public async Task<ClubDTO> Handle(AddMemberComand request, CancellationToken cancellationToken)
        {
            // Get the club from the db
            var club = await _unitOfWork.Set<Domain.ClubAggregate.Club>().ReadAsync(
                new ClubByIdSpecification(request.ClubId),
                c => c.Include(c => c.Members))
                ?? throw new KeyNotFoundException($"Club with ID {request.ClubId} was not found.");

            // Check if the specified user exists
            var user = await _unitOfWork.Set<Domain.CocktailAggregate.Cocktail>().ReadAsync(new CocktailByIdSpecification(request.UserId))
                ?? throw new KeyNotFoundException($"User with ID {request.UserId} was not found.");

            // Add the member to the club.
            club.AddMember(user.Id, request.PerformingMemberId);

            // Persist modifications
            await _unitOfWork.SaveChangesAsync();

            // And return the updated DTO
            return _autoMapper.Map<ClubDTO>(club);
        }
    }
}
