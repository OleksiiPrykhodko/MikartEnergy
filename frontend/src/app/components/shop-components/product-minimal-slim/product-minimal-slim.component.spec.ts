import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductMinimalSlimComponent } from './product-minimal-slim.component';

describe('ProductMinimalSlimComponent', () => {
  let component: ProductMinimalSlimComponent;
  let fixture: ComponentFixture<ProductMinimalSlimComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductMinimalSlimComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductMinimalSlimComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
