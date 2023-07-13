import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductMinimalWideComponent } from './product-minimal-wide.component';

describe('ProductMinimalWideComponent', () => {
  let component: ProductMinimalWideComponent;
  let fixture: ComponentFixture<ProductMinimalWideComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductMinimalWideComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductMinimalWideComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
