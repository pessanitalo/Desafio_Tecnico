import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from '../../../environments/environment';
import { map, tap } from 'rxjs/operators';
import { Consultation } from '../models/consultation';
import { ConsultationCreate } from '../models/consultation-create';
import { ConsultationObterAgendaProfissional } from '../models/consultation-obterAgendaProfissional';



@Injectable({
  providedIn: 'root'
})
export class ConsultationService {

  baseUrl = `${environment.mainUrlAPI}consulta`;

  constructor(private http: HttpClient) { }

  list(): Observable<Consultation[]> {
    return this.http.get<any>(`${this.baseUrl}`).pipe(
      map(response =>
        response.data.map((item: any) => ({
          id: item.id,
          profissionalId: item.profissionalId ?? item.ProfissionalId,
          pacienteNome: item.pacienteNome,
          profissionalNome: item.profissionalNome,
          dataConsulta: new Date(item.dataConsulta),
          horaConsulta: item.horaConsulta
        }))
      ),
      //tap(result => console.log('Resultado mapeado:', result))
    );
  }

obteragendaProfissional(profissionalid: number): Observable<ConsultationObterAgendaProfissional[]> {
  return this.http.get<{ data: ConsultationObterAgendaProfissional[] }>(
    `${this.baseUrl}/agenda-profissional/${profissionalid}`
  ).pipe(
    map(response => response.data),
    //tap(result => console.log('Agenda:', result))
  );
}

  adicionar(consulta: ConsultationCreate) {
    return this.http.post<ConsultationCreate>(this.baseUrl, consulta);
  }
}
