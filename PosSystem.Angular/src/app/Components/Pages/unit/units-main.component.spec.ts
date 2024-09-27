import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnitsMainComponent } from './units-main.component';

describe('UnitsMainComponent', () => {
  let component: UnitsMainComponent;
  let fixture: ComponentFixture<UnitsMainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UnitsMainComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UnitsMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
