import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InternalServerComponent } from './internal-server.component';

describe('InternalServerComponent', () => {
  let component: InternalServerComponent;
  let fixture: ComponentFixture<InternalServerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [InternalServerComponent]
    });
    fixture = TestBed.createComponent(InternalServerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
