// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CocktailsApp.Domain.CocktailAggregate;
using CocktailsApp.Domain.SeedWork;


namespace CocktailsApp.Domain.IngredientAggregate
{
    public class IngredientBuilder : IBuilder<Ingredient>
    {
        private Guid _creatorId;
        private Ingredient? _ingredient;
        private bool _isAlcoholic = false;
        private string? _name = null;
        private IngredientType? _type = null;

        public IngredientBuilder AddAllergen(string name)
        {
            TryCreateIngredient();

            if (_ingredient == null)
                throw new InvalidOperationException("Ingredient name, type and creator id must be specified before adding allergens");

            _ingredient.AddAllergen(name);
            return this;
        }
        public IngredientBuilder AsAlcoholic()
        {
            _isAlcoholic = true;
            return this;
        }
        public IngredientBuilder WithCreatorId(Guid creatorId)
        {
            _creatorId = creatorId;
            return this;
        }
        public IngredientBuilder WithName(string name)
        {
            _name = name;
            return this;
        }
        public IngredientBuilder WithType(IngredientType type)
        {
            _type = type;
            return this;
        }
        public Ingredient Build()
        {
            TryCreateIngredient();

            if (_ingredient == null)
                throw new ArgumentException("The name, type and creator id must be specified");

            return _ingredient;
        }

        private void TryCreateIngredient()
        {
            // Create Cocktail instance as soon as we have both required fields
            if (
                !string.IsNullOrWhiteSpace(_name) &&
                _creatorId != Guid.Empty &&
                _type != null &&
                _ingredient == null)
                _ingredient = new Ingredient(_name, (IngredientType)_type, _isAlcoholic, _creatorId);
        }
    }
}
