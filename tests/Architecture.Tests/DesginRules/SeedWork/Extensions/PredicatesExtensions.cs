// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


using Mono.Cecil;

using NetArchTest.Rules;


namespace CocktailsApp.Architecture.Tests.DesignRules.SeedWork
{
    public static class PredicatesExtensions
    {
        public static PredicateList MeetCondition(this Predicates predicates, Func<TypeDefinition, bool> condition)
        {
            var rule = new TypeDefinitionRule(condition);
            return predicates.MeetCustomRule(rule);
        }

    }
}
