import { Ingredient } from "../../ingredient/models/ingredient.model";

export interface CocktailIngredient {
  id: string;
  ingredient: Ingredient;
  quantity: number;
  unit: string;
}
