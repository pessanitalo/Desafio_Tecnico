import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Client } from '../models/cliente.model';

@Injectable({
  providedIn: 'root',
})
export class ClienteService {
  saveClient(client: Client): Observable<Client> {
    console.log('Salvando cliente:', client);
    return of(client);
  }

  getClients(): Observable<Client[]> {
    return of([
      { name: 'João Silva', email: 'joao@example.com', createdAt: new Date() },
      { name: 'Maria Souza', email: 'maria@example.com', createdAt: new Date() },
    ]);
  }
}
