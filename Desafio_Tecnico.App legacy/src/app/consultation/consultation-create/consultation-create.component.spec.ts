import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultationCreateComponent } from './consultation-create.component';

describe('ConsultationCreateComponent', () => {
  let component: ConsultationCreateComponent;
  let fixture: ComponentFixture<ConsultationCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConsultationCreateComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ConsultationCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
