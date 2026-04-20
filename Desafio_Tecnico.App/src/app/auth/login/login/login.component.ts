import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReactiveFormsModule, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { LoginService } from '../services/login.service';
import { NotificationService } from '../../../shared/services/notificationService';
import { Login } from '../models/login';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  Form!: UntypedFormGroup;

  login!: Login;

  constructor(
    private fb: UntypedFormBuilder,
    private loginService: LoginService,
    private notification: NotificationService,
  ) { }

  ngOnInit(): void {
    this.Form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      senha: ['', [Validators.required, Validators.minLength(6)]],
    });
  }


  entrar() {
    const user: Login = this.Form.value;
    this.loginService.login(user)
      .subscribe(
        sucesso => this.notification.handleSuccess({
          response: sucesso,
          redirectUrl: '/consultas-lista',
          message: 'Usuário logado com sucesso!'
        }),
        falha => this.notification.handleError(falha)
      );
  }
}
