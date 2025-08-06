import { Injectable } from '@angular/core';
import { ApiService } from '../../../core/services/api.service';
import { Observable } from 'rxjs';
import { CreateCocktailRequest } from '../models/create-cocktail.model';
import { Cocktail } from '../models/cocktail.model';

@Injectable({
  providedIn: 'root',
})
export class CocktailService {

  constructor(private apiService: ApiService) { }

  public createCocktail(cocktail: CreateCocktailRequest): Observable<Cocktail> {
    return this.apiService.withBody(cocktail).post<Cocktail>("cocktails")
  }
}
