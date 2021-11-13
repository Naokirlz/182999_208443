import { NgModule } from '@angular/core';
import { RouterModule, Routes, CanActivate } from '@angular/router';
import { LoginComponent } from './login/pages/login/login.component';
import { LogoutComponent } from './login/pages/logout/logout.component';
import { AltaUsuarioComponent } from './usuarios/pages/alta-usuario/alta-usuario.component';
import { ReportesComponent } from './reportes/pages/reportes/reportes.component';
import { VerUsuariosComponent } from './usuarios/pages/ver-usuarios/ver-usuarios.component';
import { AltaProyectoComponent } from './proyectos/pages/alta-proyecto/alta-proyecto.component';
import { VerProyectosComponent } from './proyectos/pages/ver-proyectos/ver-proyectos.component';
import { VerTareasComponent } from './tareas/pages/ver-tareas/ver-tareas.component';
import { AsignadosComponent } from './proyectos/pages/asignados/asignados.component';
import { EditarProyectoComponent } from './proyectos/pages/editar-proyecto/editar-proyecto.component';
import { IncidentesPComponent } from './proyectos/pages/incidentes/incidentes.component';
import { AltaTareaComponent } from './tareas/pages/alta-tarea/alta-tarea.component';
import { IncidentesProyectosComponent } from './reportes/pages/incidentes-proyectos/incidentes-proyectos.component';
import { IncidentesDesarrolladorComponent } from './reportes/pages/incidentes-desarrollador/incidentes-desarrollador.component';
import { DesarrolladorComponent } from './reportes/pages/desarrollador/desarrollador.component';
import { ModificarTareaComponent } from './tareas/pages/modificar-tarea/modificar-tarea.component';
import { MisIncidentesComponent } from './desarrollador/pages/mis-incidentes/mis-incidentes.component';
import { CargarIncidentesComponent } from './importaciones/pages/cargar-incidentes/cargar-incidentes.component';
import { UserLoggedGuard } from './routeGuards/user-logged.guard';
import { AdminLoggedGuard } from './routeGuards/admin-logged.guard';
import { DesarrolladorLoggedGuard } from './routeGuards/desarrollador-logged.guard';
import { TesterLoggedGuard } from './routeGuards/tester-logged.guard';
import { AltabugTesterComponent } from './tester/pages/altabug-tester/altabug-tester.component';
import { DetalleBugComponent } from './incidentes/pages/detalle-bug/detalle-bug.component';
import { UserNotLoggedGuard } from './routeGuards/user-not-logged.guard';


const routes: Routes = [
  { path: 'login',component: LoginComponent, pathMatch: 'full' , canActivate:[UserNotLoggedGuard]},
  { path: 'salir',component: LogoutComponent,},
  { path: 'usuarios',component: VerUsuariosComponent, canActivate:[UserLoggedGuard , AdminLoggedGuard]},
  { path: 'usuarios/alta', component: AltaUsuarioComponent,canActivate:[UserLoggedGuard, AdminLoggedGuard]},
  { path: 'reportes',component: ReportesComponent,canActivate:[UserLoggedGuard, AdminLoggedGuard]},
  { path: 'proyectos', component: VerProyectosComponent,canActivate:[UserLoggedGuard, AdminLoggedGuard]},
  { path: 'proyectos/alta',component: AltaProyectoComponent,canActivate:[UserLoggedGuard, AdminLoggedGuard]},
  { path: 'proyectos/:proyectoId/asignados',component: AsignadosComponent,canActivate:[UserLoggedGuard, AdminLoggedGuard]},
  { path: 'proyectos/:proyectoId/editar', component: EditarProyectoComponent,canActivate:[UserLoggedGuard, AdminLoggedGuard]},
  { path: 'proyectos/:proyectoId/incidentes',component: IncidentesPComponent,canActivate:[UserLoggedGuard, AdminLoggedGuard]},
  { path: 'proyectos/:proyectoId/tareas',component: VerTareasComponent,canActivate:[UserLoggedGuard, AdminLoggedGuard]},
  { path: 'reportes/incidentes',component: IncidentesProyectosComponent,canActivate:[UserLoggedGuard]},
  { path: 'reportes/resueltos',component: IncidentesDesarrolladorComponent,canActivate:[UserLoggedGuard]},
  { path: 'reportes/:desarrolladorId/incidentes',component: DesarrolladorComponent,canActivate:[UserLoggedGuard]},
  { path: 'importaciones',component: CargarIncidentesComponent,canActivate:[UserLoggedGuard]},
  { path: 'desarrollador',component: VerProyectosComponent,canActivate:[UserLoggedGuard,DesarrolladorLoggedGuard]},
  { path: 'desarrollador/incidentes',component: MisIncidentesComponent,canActivate:[UserLoggedGuard,DesarrolladorLoggedGuard]},
  { path: 'desarrollador/incidentes/:incidenteId',component: DetalleBugComponent,canActivate:[UserLoggedGuard,DesarrolladorLoggedGuard]},
  { path: 'desarrollador/tareas',component: VerTareasComponent,canActivate:[UserLoggedGuard,DesarrolladorLoggedGuard]},
  { path: 'tester',component: VerProyectosComponent,canActivate:[UserLoggedGuard,TesterLoggedGuard]},
  { path: 'tester/incidentes',component: MisIncidentesComponent,canActivate:[UserLoggedGuard,TesterLoggedGuard]},
  { path: 'tester/incidentes/:incidenteId',component: DetalleBugComponent,canActivate:[UserLoggedGuard,TesterLoggedGuard]},
  { path: 'tester/tareas',component: VerTareasComponent,canActivate:[UserLoggedGuard,TesterLoggedGuard]},
  { path: 'tester/altaIncidente',component: AltabugTesterComponent,canActivate:[UserLoggedGuard,TesterLoggedGuard]},
  { path: 'tareas',component: VerTareasComponent,canActivate:[UserLoggedGuard,AdminLoggedGuard]},
  { path: 'tareas/:tareaId/editar',component: ModificarTareaComponent,canActivate:[UserLoggedGuard]},
  { path: 'tareas/alta',component: AltaTareaComponent,canActivate:[UserLoggedGuard]},
  { path: '**', redirectTo: ''},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
