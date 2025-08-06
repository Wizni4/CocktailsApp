import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { IngredientService } from '../../ingredient/services/ingredient.service';
import { Observable } from 'rxjs';
import { Ingredient } from '../../ingredient/models/ingredient.model';
import { IngredientTableComponent } from './components/ingredients/ingredients.component';

@Component({
  selector: 'app-create-cocktail',
  standalone: true,
  imports: [
    CommonModule,
    IngredientTableComponent,
  ],
  templateUrl: './create-cocktail.component.html',
  styleUrl: './create-cocktail.component.css'
})
export class CreateCocktailComponent {
  ingredients$!: Observable<Ingredient[]>;

  constructor(private ingredientService: IngredientService) { }

  ngOnInit(): void {
    this.ingredients$ = this.ingredientService.getIngredients();
      this.ingredients$.subscribe(
    )
  }
}
