import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SignUpRegisterModalComponent } from './sign-up-register-modal.component';

describe('SignUpRegisterModalComponent', () => {
  let component: SignUpRegisterModalComponent;
  let fixture: ComponentFixture<SignUpRegisterModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SignUpRegisterModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SignUpRegisterModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
