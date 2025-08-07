import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomChipsComponent } from './custom-chips.component';

describe('CustomChipsComponent', () => {
  let component: CustomChipsComponent;
  let fixture: ComponentFixture<CustomChipsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomChipsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CustomChipsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
