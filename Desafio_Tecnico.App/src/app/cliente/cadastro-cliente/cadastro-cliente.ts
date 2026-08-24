import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ClienteService } from '../services/cliente-service';

@Component({
  selector: 'app-cadastro-cliente',
  imports: [ReactiveFormsModule],
  templateUrl: './cadastro-cliente.html',
  styleUrl: './cadastro-cliente.css',
})
export class CadastroCliente {
  private fb = inject(FormBuilder);
  private clienteService = inject(ClienteService);

  form: FormGroup = this.fb.group({
    name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(10)]],
    email: ['', [Validators.required, Validators.email, Validators.minLength(3), Validators.maxLength(30)]],
  });

  onSubmit() {
    if (this.form.valid) {
      this.clienteService.saveClient(this.form.value).subscribe({
        next: (client) => {
          console.log('Cliente cadastrado com sucesso:', client);
          this.form.reset();
        },
        error: (err) => console.error('Erro ao salvar:', err),
      });
    }
  }
}
