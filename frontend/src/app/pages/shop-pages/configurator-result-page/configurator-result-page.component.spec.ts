import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfiguratorResultPageComponent } from './configurator-result-page.component';

describe('ConfiguratorResultPageComponent', () => {
  let component: ConfiguratorResultPageComponent;
  let fixture: ComponentFixture<ConfiguratorResultPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConfiguratorResultPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConfiguratorResultPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
