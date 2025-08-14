// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using CocktailsApp.Domain.Common;


namespace CocktailsApp.Domain.Ingredients
{
    public class IngredientFactory : IFactory<Ingredient>
    {
        private Guid _creatorId;
        private Ingredient? _ingredient;
        private bool _isAlcoholic = false;
        private string? _name = null;
        private IngredientType _type = (IngredientType)9999;

        public IngredientFactory AddAllergen(string name)
        {
            TryCreateIngredient();

            if (_ingredient == null)
                throw new InvalidOperationException("Ingredient name, type and creator id must be specified before adding allergens");

            _ingredient.AddAllergen(name, _creatorId);
            return this;
        }
        public IngredientFactory AsAlcoholic()
        {
            _isAlcoholic = true;
            return this;
        }
        public IngredientFactory WithCreatorId(Guid creatorId)
        {
            _creatorId = creatorId;
            return this;
        }
        public IngredientFactory WithName(string name)
        {
            _name = name;
            return this;
        }
        public IngredientFactory WithType(IngredientType type)
        {
            _type = type;
            return this;
        }
        public Ingredient Build()
        {
            TryCreateIngredient();

            if (_ingredient == null)
                throw new ArgumentException("The name and creator id must be specified");

            return _ingredient;
        }

        private void TryCreateIngredient()
        {
            // Create Cocktail instance as soon as we have both required fields
            if (
                !string.IsNullOrWhiteSpace(_name) &&
                _creatorId != Guid.Empty &&
                Enum.IsDefined(_type) &&
                _ingredient == null)
                _ingredient = new Ingredient(_name, (IngredientType)_type, _isAlcoholic, _creatorId);
        }
    }
}
