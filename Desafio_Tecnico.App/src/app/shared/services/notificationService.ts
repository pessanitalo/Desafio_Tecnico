import { Injectable } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { Router } from '@angular/router';
import { ToastrService } from "ngx-toastr";


// notification.service.ts
@Injectable({ providedIn: 'root' })
export class NotificationService {
    constructor(
        private toastr: ToastrService,
        private router: Router
    ) { }

    handleSuccess<T>(options: {
        response?: T;
        form?: FormGroup;
        redirectUrl?: string;
        message?: string;
        onSuccess?: (response: T) => void;
    }) {
        const { response, form, redirectUrl, message, onSuccess } = options;

        if (form) form.reset();

        const toast = this.toastr.success(message || 'Sucesso!', 'OK');

        if (onSuccess) onSuccess(response as T);

        if (toast && redirectUrl) {
            toast.onHidden.subscribe(() => {
                this.router.navigate([redirectUrl]);
            });
        }
    }

    handleError(fail: any) {
        const errorResponse = fail.error;
        // Verifica se é um array de erros (validação)
        if (errorResponse?.errors && Array.isArray(errorResponse.errors) && errorResponse.errors.length > 0) {
            errorResponse.errors.forEach((erro: string) => {
                this.toastr.error(erro, 'Erro!');
            });
        }
        // Verifica se é uma mensagem única (do middleware)
        else if (errorResponse?.mensagem) {
            this.toastr.error(errorResponse.mensagem, 'Erro!');
        }
        // Fallback
        else {
            this.toastr.error('Erro inesperado', 'Erro!');
        }
    }
}