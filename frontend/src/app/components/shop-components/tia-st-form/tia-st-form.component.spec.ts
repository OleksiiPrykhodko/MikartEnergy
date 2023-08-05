import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TiaStFormComponent } from './tia-st-form.component';

describe('TiaStFormComponent', () => {
  let component: TiaStFormComponent;
  let fixture: ComponentFixture<TiaStFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TiaStFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TiaStFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
