import { Routes } from '@angular/router';
import { PatientCreateComponent } from './patient/patient-create/patient-create.component';
import { PatientListComponent } from './patient/patient-list/patient-list.component';
import { ProfessionalListComponent } from './professional/professional-list/professional-list.component';
import { ProfessionalCreateComponent } from './professional/professional-create/professional-create.component';
import { ConsultationListComponent } from './consultation/consultation-list/consultation-list.component';
import { ConsultationCreateComponent } from './consultation/consultation-create/consultation-create.component';
import { AgendaProfissionalComponent } from './shared/agenda-profissional/agenda-profissional.component';
import { LoginComponent } from './auth/login/login/login.component';
import { UserCreateComponent } from './auth/users/user-create/user-create.component';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  // Pacientes
  { path: 'pacientes-lista', component: PatientListComponent },
  { path: 'paciente-novo', component: PatientCreateComponent },
  // Profissionais
  { path: 'profissional-novo', component: ProfessionalCreateComponent },
  { path: 'profissionais-lista', component: ProfessionalListComponent },

  // login
  { path: 'user-novo', component: UserCreateComponent },
  { path: 'login', component: LoginComponent },


  // Consultas
  { path: 'consulta-novo', component: ConsultationCreateComponent },
  { path: 'consultas-lista', component: ConsultationListComponent },
  { path: 'agenda-profissional/:profissionalId', component: AgendaProfissionalComponent },
  { path: '**', redirectTo: 'login' },


];
