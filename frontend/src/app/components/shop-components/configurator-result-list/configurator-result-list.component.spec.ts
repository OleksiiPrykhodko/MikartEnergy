import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfiguratorResultListComponent } from './configurator-result-list.component';

describe('ConfiguratorResultListComponent', () => {
  let component: ConfiguratorResultListComponent;
  let fixture: ComponentFixture<ConfiguratorResultListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConfiguratorResultListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ConfiguratorResultListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
