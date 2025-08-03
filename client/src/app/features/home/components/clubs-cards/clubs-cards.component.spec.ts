import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClubsCardsComponent } from './clubs-cards.component';

describe('ClubsCardsComponent', () => {
  let component: ClubsCardsComponent;
  let fixture: ComponentFixture<ClubsCardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClubsCardsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClubsCardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
