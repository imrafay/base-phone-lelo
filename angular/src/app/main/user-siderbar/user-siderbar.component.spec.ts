import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserSiderbarComponent } from './user-siderbar.component';

describe('UserSiderbarComponent', () => {
  let component: UserSiderbarComponent;
  let fixture: ComponentFixture<UserSiderbarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserSiderbarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserSiderbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
