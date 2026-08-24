import { Routes } from '@angular/router';
import { ListaClientes } from './cliente/lista-clientes/lista-clientes';
import { CadastroCliente } from './cliente/cadastro-cliente/cadastro-cliente';

export const routes: Routes = [
  { path: '', redirectTo: 'lista-clientes', pathMatch: 'full' },
  { path: 'lista-clientes', component: ListaClientes },
  { path: 'cadastro-cliente', component: CadastroCliente },
];
