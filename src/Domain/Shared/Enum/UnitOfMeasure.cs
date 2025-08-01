/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Shared
{

    public enum UnitOfMeasure
    {
        // Volume
        ML, CL, L, FL_OZ, TBSP, TSP, DASH, DROP, SHOT, JIGGER, PINT, GALLON, CUP, QUART,
        // Mass
        MG, G, KG, OZ, LB,
        // Count
        PC, LEAF, SLICE, WEDGE, STICK, CLOVE, CUBE,
        // Culinary
        PINCH, SPRIG, TWIST, SCOOP, GRAIN
    }
}
