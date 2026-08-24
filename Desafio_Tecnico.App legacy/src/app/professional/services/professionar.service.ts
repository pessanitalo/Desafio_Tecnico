import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from '../../../environments/environment';
import { map } from 'rxjs/operators';
import { Professional } from '../models/professional';


@Injectable({
  providedIn: 'root'
})
export class ProfessionalService {

  baseUrl = `${environment.mainUrlAPI}profissional`;

  constructor(private http: HttpClient) { }

  list(): Observable<Professional[]> {
    return this.http.get<any>(`${this.baseUrl}`).pipe(
      map(response =>
        response.data.map((item: any) => ({
          nome: item.profissional.nome,
          cpf: item.profissional.cpf
        })) as Professional[]
      )
    );
  }

  // obterMedico(nome: string): Observable<Professional> {
  //   return this.http.get<Professional>(`${this.baseUrl}/buscarmediconome/${nome}`);
  // }

    obterMedico(cpf: string): Observable<Professional> {
      return this.http.get<any>(`${this.baseUrl}/pesquisar/${cpf}`).pipe(
        map(response => response.data as Professional)
      );
    }

  adicionar(paciente: Professional) {
    return this.http.post<Professional>(this.baseUrl, paciente);
  }
}
