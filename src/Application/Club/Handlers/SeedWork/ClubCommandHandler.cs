/*
 *Framework namespaces
 */
using AutoMapper;
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
/*
 * Domain namespaces
 */
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.CocktailAggregate;
using CocktailsApp.Domain.SeedWork;

using MediatR;

namespace CocktailsApp.Application.Club
{
    public abstract class ClubCommandHandler<TCommand>(IUnitOfWork unitOfWork, IMapper autoMapper)
        : ICommandHandler<TCommand, ClubDTO> where TCommand: ICommand<ClubDTO>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _autoMapper = autoMapper;
        public abstract Task<ClubDTO> Handle(TCommand request, CancellationToken cancellationToken);
        protected Task<Domain.ClubAggregate.Club> GetClubFromRepositoryAsync(Guid clubId, Func<IIncludable<Domain.ClubAggregate.Club>, IIncludable>? additionalIncludes = null)
        {
            // Defines the base include to manage permissions
            // Add add additional ones if specified
            Func<IIncludable<Domain.ClubAggregate.Club>, IIncludable> includes = c =>
            {
                var query = c.Include(club => club.Roles)
                             .Include(club => club.Owner)
                             .Include(club => club.Members)
                                .ThenInclude(member => member.Roles);

                return additionalIncludes != null ? additionalIncludes(query) : query;
            };

            // Get the club from the database, including related entities.
            var club = _unitOfWork.Set<Domain.ClubAggregate.Club>().ReadAsync(new ClubByIdSpecification(clubId), includes)
                ?? throw new KeyNotFoundException($"Club with ID {clubId} was not found.");

            return club;
        }
    }
}
