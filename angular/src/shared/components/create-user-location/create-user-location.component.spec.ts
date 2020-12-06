import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateUserLocationComponent } from './create-user-location.component';

describe('CreateUserLocationComponent', () => {
  let component: CreateUserLocationComponent;
  let fixture: ComponentFixture<CreateUserLocationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateUserLocationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateUserLocationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
