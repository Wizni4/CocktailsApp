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
    public interface ICocktailService : IService
    {
        Task<decimal> CalculateTotalPriceAsync(Cocktail cocktail);
        Task<decimal> CalculateTotalCostAsync(Cocktail cocktail);
    }
}