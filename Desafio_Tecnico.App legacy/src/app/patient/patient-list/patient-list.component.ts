import { Component, OnInit } from '@angular/core';
import { Patient } from '../models/patiente';
import { PatienteService } from '../services/patiente.service';

import { RouterLink } from '@angular/router';
import { NgxMaskDirective, NgxMaskPipe } from 'ngx-mask';

@Component({
    selector: 'app-patient-list',
    imports: [RouterLink, NgxMaskDirective, NgxMaskPipe],
    templateUrl: './patient-list.component.html',
    styleUrl: './patient-list.component.css'
})
export class PatientListComponent implements OnInit {

  pacientes!: Patient[];

  constructor(
    private patienteService: PatienteService,
  ) { }

  ngOnInit(): void {
    this.listapacientes();
  }

  listapacientes() {
    this.patienteService.list().subscribe(
      medicos => {
        this.pacientes = medicos;
      },
      error => console.log('Erro:', error)
    );
  }

}
