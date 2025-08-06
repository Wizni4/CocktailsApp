import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { TableModule } from 'primeng/table';
import { Ingredient } from '../../../../ingredient/models/ingredient.model';

@Component({
  selector: 'app-ingredients',
  standalone: true,
  imports: [
    CommonModule,
    TableModule,
  ],
  templateUrl: './ingredients.component.html',
  styleUrls: ['./ingredients.component.css']
})
export class IngredientTableComponent {
  @Input() ingredients: Ingredient[] = [];
}
