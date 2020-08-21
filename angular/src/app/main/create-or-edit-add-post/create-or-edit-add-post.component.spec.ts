import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrEditAddPostComponent } from './create-or-edit-add-post.component';

describe('CreateOrEditAddPostComponent', () => {
  let component: CreateOrEditAddPostComponent;
  let fixture: ComponentFixture<CreateOrEditAddPostComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateOrEditAddPostComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateOrEditAddPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
