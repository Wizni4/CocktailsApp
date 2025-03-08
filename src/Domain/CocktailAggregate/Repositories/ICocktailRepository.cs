/*
 * Domain namespaces
 */
using Domain.SeedWork;

/*
 * Framework namespaces
 */
using System;
using System.Collections.Generic;

namespace Domain.CocktailAggregate
{
    public interface ICocktailRepository : IRepository<Cocktail>
    {
    }
}