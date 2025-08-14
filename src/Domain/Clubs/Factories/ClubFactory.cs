/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Common;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Clubs
{
    /// <summary>
    /// A builder for creating instances of the <see cref="Club"/> class.
    /// </summary>
    public class ClubFactory : IFactory<Club>
    {
        private Address? _address;
        private string? _description;
        private string? _name;
        private Guid _ownerId = Guid.Empty;
        private Visibility _visibility = Visibility.Private;

        /// <summary>
        /// Sets the address for the <see cref="Club"/> being built.
        /// </summary>
        /// <param name="address">The address of the  <see cref="Club"/>.</param>
        /// <returns>The current instance of <see cref="ClubFactory"/>.</returns>
        public ClubFactory WithAddress(
            string street,
            string streetNumber,
            string city,
            string postalCode,
            string state,
            string country)
        {
            _address = new Address(
                street,
                streetNumber,
                city,
                postalCode,
                state,
                country);
            return this;
        }

        /// <summary>
        /// Sets the description for the <see cref="Club"/> being built.
        /// </summary>
        /// <param name="description">The description of the <see cref="Club"/>.</param>
        /// <returns>The current instance of <see cref="ClubFactory"/>.</returns>
        public ClubFactory WithDescription(string? description)
        {
            _description = description;
            return this;
        }

        /// <summary>
        /// Sets the name for the <see cref="Club"/> being built.
        /// </summary>
        /// <param name="name">The name of the <see cref="Club"/>.</param>
        /// <returns>The current instance of <see cref="ClubFactory"/>.</returns>
        public ClubFactory WithName(string? name)
        {
            _name = name;
            return this;
        }

        /// <summary>
        /// Sets the owner for the <see cref="Club"/> being built.
        /// </summary>
        /// <param name="ownerId">The owner Id of the <see cref="Club"/>.</param>
        /// <returns>The current instance of <see cref="ClubFactory"/>.</returns>
        public ClubFactory WithOwner(Guid ownerId)
        {
            _ownerId = ownerId;
            return this;
        }

        /// <summary>
        /// Sets the visibility for the <see cref="Club"/> being built.
        /// </summary>
        /// <param name="visibility">The visibility of the <see cref="Club"/>.</param>
        /// <returns>The current instance of <see cref="ClubFactory"/>.</returns>
        public ClubFactory WithVisibility(Visibility visibility)
        {
            _visibility = visibility;
            return this;
        }

        /// <summary>
        /// Constructs and returns a new instance of the <see cref="Club"/>
        /// </summary>
        /// <remarks>
        /// <para>
        /// Required properties:
        /// <list type="bullet">
        /// <item>Address</item>
        /// <item>Description</item>
        /// <item>Name</item>
        /// <item>Owner</item>
        /// </list>
        /// </para>
        /// <para>
        /// Optional properties:
        /// <list type="bullet">
        /// <item>
        /// Visibility - Default: <see cref="Visibility.Private"/>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <returns>
        /// A new instance of <see cref="Club"/> with specified properties:
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when any of the provided arguments are invalid (<see langword="null"/> or <see langword="empty"/>).
        /// </exception>
        public Club Build()
        {
            var club = new Club(
                _address,
                _description,
                _name,
                _ownerId,
                _visibility);

            return club;
        }
    }
}
