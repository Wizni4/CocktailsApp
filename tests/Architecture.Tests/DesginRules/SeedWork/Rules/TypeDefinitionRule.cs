// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using Mono.Cecil;

using NetArchTest.Rules;


namespace CocktailsApp.Architecture.Tests.DesignRules.SeedWork
{
    public class TypeDefinitionRule(Func<TypeDefinition, bool> condition) : ICustomRule
    {
        private readonly Func<TypeDefinition, bool> _condition = condition;
        public bool MeetsRule(TypeDefinition type)
        {
            return _condition(type);
        }
    }
}
