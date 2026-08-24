import { Component, OnInit, inject } from '@angular/core';
import { DatePipe } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { RouterLink } from '@angular/router';
import { ClienteService } from '../services/cliente-service';
import { Client } from '../models/cliente.model';

@Component({
  selector: 'app-lista-clientes',
  imports: [MatTableModule, DatePipe, RouterLink],
  templateUrl: './lista-clientes.html',
  styleUrl: './lista-clientes.css',
})
export class ListaClientes implements OnInit {
  private clienteService = inject(ClienteService);
  clients: Client[] = [];
  displayedColumns: string[] = ['name', 'email', 'createdAt'];

  ngOnInit() {
    this.clienteService.getClients().subscribe(clients => {
      this.clients = clients;
    });
  }
}
