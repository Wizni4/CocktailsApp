import { Injectable } from '@angular/core';
import { ApiService } from '../../../core/services/api.service';
import { Observable } from 'rxjs';
import { Ingredient } from '../models/ingredient.model';

@Injectable({
  providedIn: 'root',
})
export class IngredientService {

  constructor(private apiService: ApiService) { }

  public getIngredients(): Observable<Ingredient[]> {
    return this.apiService.get<Ingredient[]>("ingredients")
  }

  public createIngredient(ingredient: Ingredient): Observable<Ingredient> {
    return this.apiService.withBody(ingredient).post<Ingredient>("ingredients")
  }
}
