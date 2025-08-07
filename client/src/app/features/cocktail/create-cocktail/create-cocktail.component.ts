import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { CommonModule } from '@angular/common';
import { AbstractControl, FormArray, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Component } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Ingredient } from '../../ingredient/models/ingredient.model';
import { IngredientService } from '../../ingredient/services/ingredient.service';
import { CreateCocktailRequest } from '../models/create-cocktail.model';
import { CocktailService } from '../services/cocktail.service';
import { CreateIngredientRequest } from '../../ingredient/models/create-ingredient.model';
import { CustomInputComponent } from '../../../shared/components/custom-input/custom-input.component';
import { CustomTextAreaInput } from '../../../shared/components/custom-textarea/custom-textarea.component';
import { SubmitButtonComponent } from '../../../shared/components/submit-button/submit-button.component';
import { CustomChipsComponent } from '../../../shared/components/custom-chips/custom-chips.component';
import { CustomDropdownComponent } from '../../../shared/components/custom-dropdown/custom-dropdown.component';
import { UnitService } from '../../../shared/services/unit.service';
import { TableModule } from 'primeng/table';

@Component({
  selector: 'app-create-cocktail',
  standalone: true,
  imports: [
    CommonModule,
    InputTextModule,
    TextareaModule,
    ButtonModule,
    DialogModule,
    DialogModule,
    CustomChipsComponent,
    CustomDropdownComponent,
    CustomInputComponent,
    CustomTextAreaInput,
    SubmitButtonComponent,
    ReactiveFormsModule,
    TableModule,
  ],
  templateUrl: './create-cocktail.component.html',
  styleUrl: './create-cocktail.component.css'
})
export class CreateCocktailComponent {
  createCocktailForm: FormGroup;
  createIngredientForm: FormGroup;

  ingredients$ = new BehaviorSubject<Ingredient[]>([]);

  ingredientTypes$ = new Observable<string[]>();
  unitsOfMeasure$ = new Observable<string[]>();
  createCocktailRequest: CreateCocktailRequest = {} as CreateCocktailRequest;
  createIngredientRequest: CreateIngredientRequest = {} as CreateIngredientRequest;

  errMessage: string = '';
  loading: boolean = false;
  showAddIngredientForm = false;

  constructor(
    private ingredientService: IngredientService,
    private cocktailService: CocktailService,
    private unitService: UnitService,
    private fb: FormBuilder,
  ) {
    this.createCocktailForm = this.fb.group({
      name: ['', Validators.required],
      description: [null],
      ingredients: this.fb.array([])
    })

    this.createIngredientForm = this.fb.group({
      name: ['', Validators.required],
      type: ['', Validators.required],
      isAlcoholic: ['', Validators.required],
      allergens: this.fb.control<string[] | null>(null)
    })
  }

  ngOnInit(): void {
    this.ingredientService.getIngredients().subscribe(ingredients => {
      this.ingredients$.next(ingredients); // load initial data
    });
    this.ingredientTypes$ = this.ingredientService.getIngredientTypes();
    this.unitsOfMeasure$ = this.unitService.getUnitsOfMeasure();
  }

  get ingredientsFormArray(): FormArray {
    return this.createCocktailForm.get('ingredients') as FormArray;
  }

  get availableIngredients(): Ingredient[] {
    const allIngredients = this.ingredients$.getValue();
    const addedIngredients = new Set(
      this.ingredientsFormArray.controls.map(ctrl => ctrl.value.id)
    );
    return allIngredients.filter(i => !addedIngredients.has(i.id));
  }

  addIngredient(ingredient: any): void {
    const ingredientsFormArray = this.createCocktailForm.get('ingredients') as FormArray;

    // Check if already added (based on ID)
    const alreadyAdded = ingredientsFormArray.controls.some(ctrl => ctrl.value.id === ingredient.id);
    if (alreadyAdded) return;

    // Create a FormGroup for the new ingredient
    const ingredientGroup = this.fb.group({
      id: [ingredient.id],
      name: [ingredient.name],
      type: [ingredient.type],
      quantity: [0, Validators.required],
      unit: ['', Validators.required]
    });

    ingredientsFormArray.push(ingredientGroup);
  }

  removeIngredient(control: AbstractControl) {
    const index = this.ingredientsFormArray.controls.indexOf(control);
    if (index !== -1) {
      this.ingredientsFormArray.removeAt(index);
    }
  }

  createIngredient() {
    this.errMessage = '';
    this.loading = true;

    if (this.createIngredientForm.invalid) {
      this.createIngredientForm.markAllAsTouched();
      this.loading = false;
      return;
    }

    var rawForm = this.createIngredientForm.value
    this.createIngredientRequest = {
      ...rawForm,
      isAlcoholic: rawForm.isAlcoholic === "true",
    };
    this.ingredientService.createIngredient(this.createIngredientRequest).subscribe({
      next: newIngredient => {
        const currentIngredients = this.ingredients$.getValue();
        this.ingredients$.next([...currentIngredients, newIngredient]);
        this.showAddIngredientForm = false;
        this.loading = false;
      },
      error: err => {
        this.errMessage = err?.error?.detail || 'Creation failed. Please try again.';
        this.loading = false;
      },
    });
    this.showAddIngredientForm = false;
  }

  createCocktail() {
    this.errMessage = '';
    this.loading = true;

    if (this.createCocktailForm.invalid) {
      this.createCocktailForm.markAllAsTouched();
      this.loading = false;
      return;
    }
    this.createCocktailRequest = this.createCocktailForm.value;
    console.log(this.createCocktailRequest)
    this.cocktailService.createCocktail(this.createCocktailRequest).subscribe({
      next: () => {
        this.loading = false;
      },
      error: err => {
        this.errMessage = err?.error?.detail || 'Creation failed. Please try again.';
        this.loading = false;
      },
    });
  }
}
