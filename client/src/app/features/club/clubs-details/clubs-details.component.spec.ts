import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClubsDetailsComponent } from './clubs-details.component';

describe('ClubsDetailsComponent', () => {
  let component: ClubsDetailsComponent;
  let fixture: ComponentFixture<ClubsDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClubsDetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClubsDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
