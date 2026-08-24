import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from '../../../environments/environment';
import { map } from 'rxjs/operators';
import { Patient } from '../models/patiente';


@Injectable({
  providedIn: 'root'
})
export class PatienteService {

  baseUrl = `${environment.mainUrlAPI}paciente`;

  constructor(private http: HttpClient) { }

  list(): Observable<Patient[]> {
    return this.http.get<any>(`${this.baseUrl}`).pipe(
      map(response =>
        response.data.map((item: any) => ({
          nome: item.paciente.nome,
          cpf: item.paciente.cpf
        })) as Patient[]
      )
    );
  }

  buscarpacientePorNome(cpf: string): Observable<Patient> {
    return this.http.get<any>(`${this.baseUrl}/pesquisar/${cpf}`).pipe(
      map(response => response.data as Patient)
    );
  }

  adicionar(paciente: Patient) {
    return this.http.post<Patient>(this.baseUrl, paciente);
  }
}
