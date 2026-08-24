import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute, convertToParamMap } from '@angular/router';
import { of } from 'rxjs';

import { AgendaProfissionalComponent } from './agenda-profissional.component';
import { ConsultationService } from '../../consultation/services/consultation.service';

describe('AgendaProfissionalComponent', () => {
  let component: AgendaProfissionalComponent;
  let fixture: ComponentFixture<AgendaProfissionalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AgendaProfissionalComponent],
      providers: [
        {
          provide: ConsultationService,
          useValue: {
            obteragendaProfissional: () => of([])
          }
        },
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              paramMap: convertToParamMap({ profissionalId: '2' })
            }
          }
        }
      ]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AgendaProfissionalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
