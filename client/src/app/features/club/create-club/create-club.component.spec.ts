import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateClubComponent } from './create-club.component';

describe('CreateClubComponent', () => {
  let component: CreateClubComponent;
  let fixture: ComponentFixture<CreateClubComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateClubComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateClubComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
