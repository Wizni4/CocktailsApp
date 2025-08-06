import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateCocktailComponent } from './create-cocktail.component';

describe('CreateCocktailComponent', () => {
  let component: CreateCocktailComponent;
  let fixture: ComponentFixture<CreateCocktailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateCocktailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateCocktailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
