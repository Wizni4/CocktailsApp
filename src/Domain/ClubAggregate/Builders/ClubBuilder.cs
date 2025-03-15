/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
using CocktailsApp.Domain.Shared;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.ClubAggregate
{
    public class ClubBuilder : IBuilder<Club>
    {
        private Address? _address;
        private string? _description;
        private string? _name;
        private Guid _ownerId = Guid.Empty;
        private ClubVisibility _visibility = ClubVisibility.Private;

        public ClubBuilder WithAddress(Address address)
        {
            _address = address;
            return this;
        }

        public ClubBuilder WithDescription(string? description)
        {
            _description = description;
            return this;
        }

        public ClubBuilder WithName(string? name)
        {
            _name = name;
            return this;
        }

        public ClubBuilder WithOwner(Guid ownerId)
        {
            _ownerId = ownerId;
            return this;
        }

        public ClubBuilder WithVisibilityr(ClubVisibility visibility)
        {
            _visibility = visibility;
            return this;
        }

        public Club Build()
        {
            return new Club(
                _address,
                _description,
                _name,
                _ownerId,
                _visibility);
        }
    }
}
