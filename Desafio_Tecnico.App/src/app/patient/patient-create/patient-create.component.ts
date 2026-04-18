import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Patient } from '../models/patiente';
import { ReactiveFormsModule, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Router, RouterLink } from '@angular/router';
import { PatienteService } from '../services/patiente.service';
import { NgxMaskDirective, NgxMaskPipe } from 'ngx-mask';
import { NotificationService } from '../../shared/services/notificationService';

@Component({
  selector: 'app-patient-create',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, NgxMaskDirective, NgxMaskPipe],
  templateUrl: './patient-create.component.html',
  styleUrl: './patient-create.component.css'
})
export class PatientCreateComponent {

  Form!: UntypedFormGroup;

  paciente!: Patient;

  constructor(
    private fb: UntypedFormBuilder,
    private toastr: ToastrService,
    private pacienteService: PatienteService,
    private notification: NotificationService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.Form = this.fb.group({
      paciente: this.fb.group({
        nome: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
        cpf: ['', [Validators.required, Validators.minLength(11), Validators.maxLength(11)]],
      })
    });
  }

  adicionar() {
    this.paciente = Object.assign({}, this.paciente, this.Form.value);
    this.pacienteService.adicionar(this.paciente)
      .subscribe(
        sucesso => this.notification.handleSuccess({
          response: sucesso,
          redirectUrl: '/pacientes-lista',
          message: 'Paciente cadastrado com sucesso!'
        }),
        falha => this.notification.handleError(falha)
      );
  }

}
