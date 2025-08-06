import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IngredientTableComponent } from './ingredients.component';

describe('IngredientTableComponent', () => {
  let component: IngredientTableComponent;
  let fixture: ComponentFixture<IngredientTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [IngredientTableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IngredientTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
