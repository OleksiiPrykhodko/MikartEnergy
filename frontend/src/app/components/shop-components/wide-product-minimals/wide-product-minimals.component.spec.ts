import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WideProductMinimalsComponent } from './wide-product-minimals.component';

describe('WideProductMinimalsComponent', () => {
  let component: WideProductMinimalsComponent;
  let fixture: ComponentFixture<WideProductMinimalsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WideProductMinimalsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WideProductMinimalsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
