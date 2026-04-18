import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Consultation } from '../models/consultation';
import { ConsultationService } from '../services/consultation.service';


interface Consulta {
  id: number;
  pacienteNome: string;
  profissionalNome: string;
  dataConsulta: string;
  horaConsulta: string;
}

@Component({
  selector: 'app-consultation-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './consultation-list.component.html',
  styleUrl: './consultation-list.component.css'
})
export class ConsultationListComponent implements OnInit {

  consultations: Consultation[] = [];
  errorMessage: string = '';

  constructor(
    private consultationService: ConsultationService,
  ) { }

  ngOnInit(): void {
    this.listaConsultas();
  }

  listaConsultas() {
    this.consultationService.list().subscribe(
      consultations => {
        this.consultations = consultations;
        //console.log('Consultas recebidas:', this.consultations);
      },
      error => console.log('Erro:', error)
    );
  }

}

