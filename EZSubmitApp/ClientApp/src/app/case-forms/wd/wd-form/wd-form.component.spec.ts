import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WdFormComponent } from './wd-form.component';

describe('WdFormComponent', () => {
  let component: WdFormComponent;
  let fixture: ComponentFixture<WdFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WdFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WdFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
