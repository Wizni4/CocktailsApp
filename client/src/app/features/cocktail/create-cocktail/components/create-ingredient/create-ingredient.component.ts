import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-create-ingredient',
  imports: [],
  templateUrl: './create-ingredient.component.html',
  styleUrl: './create-ingredient.component.css'
})
export class CreateIngredientComponent {
  ingredientForm: FormGroup;
  loading: boolean = false;
  errMessage: string = "";
  constructor(private fb: FormBuilder) {
    this.ingredientForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', [
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$/)
      ]]
    })
  }

  createIngredient(): void {

  }
}
