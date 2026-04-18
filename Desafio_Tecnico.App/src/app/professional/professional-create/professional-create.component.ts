import { Component } from '@angular/core';
import { ReactiveFormsModule, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Professional } from '../models/professional';
import { CommonModule } from '@angular/common';
import { ProfessionalService } from '../services/professionar.service';
import { NgxMaskDirective, NgxMaskPipe } from 'ngx-mask';
import { NotificationService } from '../../shared/services/notificationService';

@Component({
  selector: 'app-professional-create',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, NgxMaskDirective, NgxMaskPipe],
  templateUrl: './professional-create.component.html',
  styleUrl: './professional-create.component.css'
})
export class ProfessionalCreateComponent {
  Form!: UntypedFormGroup;

  professional!: Professional;

  constructor(
    private fb: UntypedFormBuilder,
    private toastr: ToastrService,
    private professionalService: ProfessionalService,
    private notification: NotificationService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.Form = this.fb.group({
      profissional: this.fb.group({
        nome: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
        cpf: ['', [Validators.required, Validators.minLength(11), Validators.maxLength(11)]],
      })
    });
  }

  adicionar() {
    this.professionalService.adicionar(this.Form.value)
      .subscribe(
        sucesso => this.notification.handleSuccess({
          response: sucesso,
          redirectUrl: '/profissionais-lista',
          message: 'Profissional cadastrado com sucesso!'
        }),
        falha => this.notification.handleError(falha)
      );
  }

}
