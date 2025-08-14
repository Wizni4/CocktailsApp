// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Domain.Common;


namespace CocktailsApp.Domain.Ingredients
{
    public sealed class Allergen : ValueObject
    {
        public string Name { get => _name; }
        private readonly string _name = null!;
        private Allergen() { }
        internal Allergen(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Allergen name cannot be null or empty.");

            _name = name;
        }
    }
}
