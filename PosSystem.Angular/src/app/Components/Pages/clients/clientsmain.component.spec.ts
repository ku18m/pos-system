import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientsmainComponent } from './clientsmain.component';

describe('ClientsmainComponent', () => {
  let component: ClientsmainComponent;
  let fixture: ComponentFixture<ClientsmainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClientsmainComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClientsmainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
