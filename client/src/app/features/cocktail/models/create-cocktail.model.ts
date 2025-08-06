export interface CreateCocktailRequest {
  description: string;
  ingredients: CreateCocktailIngredientRequest[];
  name: string;
}

export interface CreateCocktailIngredientRequest {
  id: string;
  quantity: number;
  unit: string;
}
