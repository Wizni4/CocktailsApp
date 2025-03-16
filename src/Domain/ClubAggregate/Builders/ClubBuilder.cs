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
    /// <summary>
    /// A builder for creating instances of the <see cref="Club"/> class.
    /// </summary>
    public class ClubBuilder : IBuilder<Club>
    {
        private Address? _address;
        private string? _description;
        private string? _name;
        private Guid _ownerId = Guid.Empty;
        private ClubVisibility _visibility = ClubVisibility.Private;

        /// <summary>
        /// Sets the address for the <see cref="Club"/> being built.
        /// </summary>
        /// <param name="address">The address of the  <see cref="Club"/>.</param>
        /// <returns>The current instance of <see cref="ClubBuilder"/>.</returns>
        public ClubBuilder WithAddress(Address address)
        {
            _address = address;
            return this;
        }

        /// <summary>
        /// Sets the description for the <see cref="Club"/> being built.
        /// </summary>
        /// <param name="description">The description of the <see cref="Club"/>.</param>
        /// <returns>The current instance of <see cref="ClubBuilder"/>.</returns>
        public ClubBuilder WithDescription(string? description)
        {
            _description = description;
            return this;
        }

        /// <summary>
        /// Sets the name for the <see cref="Club"/> being built.
        /// </summary>
        /// <param name="name">The name of the <see cref="Club"/>.</param>
        /// <returns>The current instance of <see cref="ClubBuilder"/>.</returns>
        public ClubBuilder WithName(string? name)
        {
            _name = name;
            return this;
        }

        /// <summary>
        /// Sets the owner for the <see cref="Club"/> being built.
        /// </summary>
        /// <param name="ownerId">The owner Id of the <see cref="Club"/>.</param>
        /// <returns>The current instance of <see cref="ClubBuilder"/>.</returns>
        public ClubBuilder WithOwner(Guid ownerId)
        {
            _ownerId = ownerId;
            return this;
        }

        /// <summary>
        /// Sets the visibility for the <see cref="Club"/> being built.
        /// </summary>
        /// <param name="visibility">The visibility of the <see cref="Club"/>.</param>
        /// <returns>The current instance of <see cref="ClubBuilder"/>.</returns>
        public ClubBuilder WithVisibility(ClubVisibility visibility)
        {
            _visibility = visibility;
            return this;
        }

        /// <summary>
        /// Constructs and returns a new instance of the <see cref="Club"/>
        /// </summary>
        /// <remarks>
        /// Required properties:
        /// <list type="bullet">
        /// <item>Address</item>
        /// <item>Description</item>
        /// <item>Name</item>
        /// <item>Owner</item>
        /// </list>
        /// </remarks>
        /// <returns>
        /// A new instance of <see cref="Club"/> with specified properties:
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when any of the provided arguments are invalid (<see langword="null"/> or <see langword="empty"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the builder is in an invalid state, such as missing required properties.
        /// </exception>
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
