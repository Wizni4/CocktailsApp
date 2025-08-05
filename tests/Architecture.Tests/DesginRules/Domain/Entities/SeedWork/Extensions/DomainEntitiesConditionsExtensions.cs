// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Domain.SeedWork;
using NetArchTest.Rules;


namespace CocktailsApp.Architecture.Tests.DesignRules.Domain
{
    public static class DomainEntitiesConditionsExtensions
    {
        public static ConditionList BeEntities(this Conditions conditions)
        {
            return conditions
                .Inherit(typeof(Entity))
                .And()
                .NotInherit(typeof(AggregateRoot))
                .And()
                .NotImplementInterface(typeof(IAggregateRoot));
        }

        public static ConditionList BeAggregates(this Conditions conditions)
        {
            return conditions.Inherit(typeof(AggregateRoot));
        }

        public static ConditionList BeValueObjects(this Conditions conditions)
        {
            return conditions.Inherit(typeof(ValueObject));
        }
    }
}
