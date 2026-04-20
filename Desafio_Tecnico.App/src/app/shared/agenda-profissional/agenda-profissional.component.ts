import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ConsultationObterAgendaProfissional } from '../../consultation/models/consultation-obterAgendaProfissional';
import { ConsultationService } from '../../consultation/services/consultation.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxMaskDirective, NgxMaskPipe } from 'ngx-mask';


@Component({
  selector: 'app-agenda-profissional',
  standalone: true,
  imports: [CommonModule, CommonModule, ReactiveFormsModule, RouterLink, NgxMaskDirective, NgxMaskPipe, FormsModule],
  templateUrl: './agenda-profissional.component.html',
  styleUrl: './agenda-profissional.component.css'
})
export class AgendaProfissionalComponent implements OnInit {
  agenda: ConsultationObterAgendaProfissional[] = [];
  nomeProfissional = '';
  profissionalId = 2;
  carregando = false;
  errorMessage = '';

  constructor(
    private consultationService: ConsultationService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    const profissionalIdParam = this.route.snapshot.paramMap.get('profissionalId');
    const profissionalIdConvertido = Number(profissionalIdParam);

    if (profissionalIdParam && !Number.isNaN(profissionalIdConvertido)) {
      this.profissionalId = profissionalIdConvertido;
    }

    this.listaConsultas();
  }

  listaConsultas() {
    this.consultationService.obteragendaProfissional(this.profissionalId).subscribe(
      consultations => {
        this.agenda = consultations;
        this.nomeProfissional = this.agenda[0]?.nomeProfissional || '';
      },
      error => console.log('Erro:', error)
    );
  }

}
