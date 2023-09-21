import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SlimProductMinimalsComponent } from './slim-product-minimals.component';

describe('SlimProductMinimalsComponent', () => {
  let component: SlimProductMinimalsComponent;
  let fixture: ComponentFixture<SlimProductMinimalsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SlimProductMinimalsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SlimProductMinimalsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
