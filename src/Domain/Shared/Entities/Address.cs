/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Shared
{
    public class Address : ValueObject
    {
        public string Street { get; }
        public string StreetNumber { get; }
        public string City { get; }
        public string PostalCode { get; }
        public string State { get; }
        public string Country { get; }
        internal Address(string street, string streetNumber, string city, string postalCode, string state, string country)
        {
            Street = street;
            StreetNumber = streetNumber;
            City = city;
            PostalCode = postalCode;
            State = state;
            Country = country;
        }
    }
}
