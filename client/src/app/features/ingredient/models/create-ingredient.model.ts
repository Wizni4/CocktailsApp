export interface CreateIngredientRequest {
  allergens: string[];
  name: string;
  type: string;
  isAlcoholic: boolean;
  image: string;
}
