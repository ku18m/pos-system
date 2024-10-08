import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersMainComponent } from './users-main.component';

describe('UsersMainComponent', () => {
  let component: UsersMainComponent;
  let fixture: ComponentFixture<UsersMainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UsersMainComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UsersMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
