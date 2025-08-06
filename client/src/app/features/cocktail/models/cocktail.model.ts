import { CocktailIngredient } from "./cocktail-ingredient.model";

export interface Cocktail {
  id: string;
  description: string;
  ingredients: CocktailIngredient[];
  name: string;
}
