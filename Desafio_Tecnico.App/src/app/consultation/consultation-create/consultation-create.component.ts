import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AbstractControl, ReactiveFormsModule, UntypedFormBuilder, UntypedFormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { NgxMaskDirective, NgxMaskPipe } from 'ngx-mask';
import { ToastrService } from 'ngx-toastr';
import { Patiente } from '../../patient/models/patiente';
import { PatienteService } from '../../patient/services/patiente.service';
import { Professional } from '../../professional/models/professional';
import { ProfessionalService } from '../../professional/services/professionar.service';
import { Consultation } from '../models/consultation';
import { ConsultationCreate } from '../models/consultation-create';
import { ConsultationService } from '../services/consultation.service';
import { HorarioService } from '../services/horario.service';
import { NotificationService } from '../../shared/services/notificationService';

@Component({
  selector: 'app-consultation-create',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, NgxMaskDirective, NgxMaskPipe],
  templateUrl: './consultation-create.component.html',
  styleUrl: './consultation-create.component.css'
})
export class ConsultationCreateComponent {

  Form!: UntypedFormGroup;
  paciente!: Patiente;
  medico!: Professional;
  horarios: string[] = [];
  consulta!: Consultation;

  pacienteId: number = 0;
  medicoId: number = 0;

  constructor(
    private fb: UntypedFormBuilder,
    private toastr: ToastrService,
    private pacienteService: PatienteService,
    private professionalService: ProfessionalService,
    private notification: NotificationService,
    private consultaService: ConsultationService,
    private horarioService: HorarioService,
    private router: Router,
  ) { }

  private dataAtualOuFuturaValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const value = control.value;

      if (!value || typeof value !== 'string' || value.length !== 10) {
        return null;
      }

      const [dia, mes, ano] = value.split('/');
      const dataInformada = new Date(Number(ano), Number(mes) - 1, Number(dia));

      if (Number.isNaN(dataInformada.getTime())) {
        return { dataInvalida: true };
      }

      const hoje = new Date();
      hoje.setHours(0, 0, 0, 0);
      dataInformada.setHours(0, 0, 0, 0);

      return dataInformada < hoje ? { dataMenorQueHoje: true } : null;
    };
  }

  ngOnInit(): void {
    this.Form = this.fb.group({
      cpfProfissional: ['', [Validators.required]],
      medicoNome: ['', [Validators.required]],
      cpfPaciente: ['', [Validators.required]],
      pacienteNome: ['', [Validators.required]],
      dataConsulta: ['', [Validators.required, this.dataAtualOuFuturaValidator()]],
      horaConsulta: ['', [Validators.required]],
    });
    this.carregarHorarios();
    console.log(this.Form.value);
  }

  buscarPaciente() {
    const cpfPaciente = this.Form.get('cpfPaciente')?.value;

    if (!cpfPaciente) {
      this.Form.get('cpfPaciente')?.markAsTouched();
      this.toastr.warning('Informe o CPF do paciente.', 'Ops!');
      return;
    }

    this.pacienteService.buscarpacientePorNome(cpfPaciente).subscribe((res) => {
      this.paciente = res;
      this.pacienteId = this.paciente.pacienteId;
      this.Form.patchValue({
        pacienteNome: this.paciente.nome || ''
      });
      this.toastr.success('Paciente adicionado com sucesso.', 'Sucesso!');
    },
      falha => { this.processarFalha(falha); }
    );
  }

  onSubmit(): void {
    if (this.Form.invalid) {
      this.Form.markAllAsTouched();
      this.toastr.warning('Preencha todos os campos obrigatórios.', 'Ops!');
      return;
    }

    const dataInput = this.Form.get('dataConsulta')?.value;
    const [dia, mes, ano] = dataInput.split('/');
    const dataFormatada = `${ano}-${mes}-${dia}`;

    const horaInput = this.Form.get('horaConsulta')?.value;
    const horaFormatada = `${horaInput}:00`;

    const dados: ConsultationCreate = {
      pacienteId: this.pacienteId,
      profissionalId: this.medicoId,
      dataConsulta: dataFormatada,
      horaConsulta: horaFormatada,
    };

    this.consultaService.adicionar(dados).subscribe(
      (sucesso) => this.notification.handleSuccess({
        response: sucesso,
        form: this.Form,
        redirectUrl: '/consultas-lista',
        message: 'Consulta agendada com sucesso!',
        onSuccess: () => {
          this.pacienteId = 0;
          this.medicoId = 0;
        }
      }),
      (falha) => this.notification.handleError(falha)
    );
  }

  private carregarHorarios(): void {
    this.horarios = this.horarioService.gerarHorarios();
  }

  buscarMedico() {
    const cpfProfissional = this.Form.get('cpfProfissional')?.value;

    if (!cpfProfissional) {
      this.Form.get('cpfProfissional')?.markAsTouched();
      this.toastr.warning('Informe o CPF do medico.', 'Ops!');
      return;
    }

    this.professionalService.obterMedico(cpfProfissional).subscribe((res) => {
      this.medico = res;
      this.medicoId = this.medico.profissionalId;
      this.Form.patchValue({
        medicoNome: this.medico.nome || ''
      });
      this.toastr.success('Medico adicionado com sucesso.', 'Sucesso!');
    },
      falha => { this.processarFalha(falha); }
    );
  }


  processarFalha(fail: any) {
    const errors = fail.error?.errors;
    if (errors && errors.length) {
      errors.forEach((erro: string) => {
        this.toastr.error(erro, 'Erro!');
      });
    } else {
      this.toastr.error('Erro inesperado', 'Erro!');
    }
  }

}
