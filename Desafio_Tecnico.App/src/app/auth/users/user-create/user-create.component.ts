import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReactiveFormsModule, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { NotificationService } from '../../../shared/services/notificationService';
import { Router } from '@angular/router';
import { User } from '../models/user';

@Component({
  selector: 'app-user-create',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './user-create.component.html',
  styleUrl: './user-create.component.css'
})
export class UserCreateComponent {
  Form!: UntypedFormGroup;

  user!: User;

  constructor(
    private fb: UntypedFormBuilder,
    private userService: UserService,
    private notification: NotificationService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.Form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      senha: ['', [Validators.required, Validators.minLength(6)]],
    })
  }

  // ok
  adicionar() {
    const user: User = this.Form.value;
    this.userService.adicionar(user)
      .subscribe(
        sucesso => this.notification.handleSuccess({
          response: sucesso,
          redirectUrl: '/pacientes-lista',
          message: 'Usuário cadastrado com sucesso!'
        }),
        falha => this.notification.handleError(falha)
      );
  }
  
}
