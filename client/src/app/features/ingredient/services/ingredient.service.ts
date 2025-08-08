import { Injectable } from '@angular/core';
import { ApiService } from '../../../core/services/api.service';
import { Observable } from 'rxjs';
import { Ingredient } from '../models/ingredient.model';
import { CreateIngredientRequest } from '../models/create-ingredient.model';

@Injectable({
  providedIn: 'root',
})
export class IngredientService {

  constructor(private apiService: ApiService) { }

  public getIngredients(): Observable<Ingredient[]> {
    return this.apiService.get<Ingredient[]>("ingredients")
  }

  public getIngredientTypes(): Observable<string[]> {
    return this.apiService.get<string[]>("ingredients/types")
  }

  public createIngredient(ingredient: CreateIngredientRequest): Observable<Ingredient> {
    return this.apiService.withBody(ingredient).post<Ingredient>("ingredients")
  }

  public uploadImage(ingredientId: string, formData: FormData): Observable<string> {
    return this.apiService.withFormData(formData).post<string>(`ingredients/${ingredientId}/image`)
  }
}
