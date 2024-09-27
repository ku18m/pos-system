import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TypesMainComponent } from './types-main.component';

describe('TypesMainComponent', () => {
  let component: TypesMainComponent;
  let fixture: ComponentFixture<TypesMainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TypesMainComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TypesMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
