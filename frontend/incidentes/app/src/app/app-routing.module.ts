import { NgModule } from '@angular/core';
import { RouterModule, Routes, CanActivate } from '@angular/router';
import { LoginComponent } from './login/pages/login/login.component';
import { LogoutComponent } from './login/pages/logout/logout.component';
import { AltaUsuarioComponent } from './usuarios/pages/alta-usuario/alta-usuario.component';
import { ReportesComponent } from './reportes/pages/reportes/reportes.component';
import { ProyectosComponent } from './proyectos/pages/proyectos/proyectos.component';
import { IncidentesComponent } from './incidentes/pages/incidentes/incidentes.component';
import { EstadosComponent } from './estados/pages/estados/estados.component';
import { AsociacionesComponent } from './asociaciones/pages/asociaciones/asociaciones.component';
import { VerUsuariosComponent } from './usuarios/pages/ver-usuarios/ver-usuarios.component';
import { AltaProyectoComponent } from './proyectos/pages/alta-proyecto/alta-proyecto.component';
import { VerProyectosComponent } from './proyectos/pages/ver-proyectos/ver-proyectos.component';
import { VerTareasComponent } from './tareas/pages/ver-tareas/ver-tareas.component';
import { AsignadosComponent } from './proyectos/pages/asignados/asignados.component';
import { EditarProyectoComponent } from './proyectos/pages/editar-proyecto/editar-proyecto.component';
import { IncidentesPComponent } from './proyectos/pages/incidentes/incidentes.component';
import { TareaspComponent } from './proyectos/pages/tareasp/tareasp.component';
import { AltaTareaComponent } from './tareas/pages/alta-tarea/alta-tarea.component';
import { IncidentesProyectosComponent } from './reportes/pages/incidentes-proyectos/incidentes-proyectos.component';
import { IncidentesDesarrolladorComponent } from './reportes/pages/incidentes-desarrollador/incidentes-desarrollador.component';
import { DesarrolladorComponent } from './reportes/pages/desarrollador/desarrollador.component';
import { ModificarTareaComponent } from './tareas/pages/modificar-tarea/modificar-tarea.component';
import { MisIncidentesComponent } from './desarrollador/pages/mis-incidentes/mis-incidentes.component';
import { CargarIncidentesComponent } from './importaciones/pages/cargar-incidentes/cargar-incidentes.component';
import { UserLoggedGuard } from './routeGuards/user-logged.guard';


const routes: Routes = [
  { path: 'login',component: LoginComponent, pathMatch: 'full'},
  { path: 'salir',component: LogoutComponent,},
  { path: 'usuarios',component: VerUsuariosComponent, canActivate:[UserLoggedGuard]},
  { path: 'usuarios/alta', component: AltaUsuarioComponent,canActivate:[UserLoggedGuard]},
  { path: 'reportes',component: ReportesComponent,canActivate:[UserLoggedGuard]},
  { path: 'proyectos', component: VerProyectosComponent,canActivate:[UserLoggedGuard]},
  { path: 'proyectos/alta',component: AltaProyectoComponent,canActivate:[UserLoggedGuard]},
  { path: 'proyectos/:proyectoId/asignados',component: AsignadosComponent,canActivate:[UserLoggedGuard]},
  { path: 'proyectos/:proyectoId/editar', component: EditarProyectoComponent,canActivate:[UserLoggedGuard]},
  { path: 'proyectos/:proyectoId/incidentes',component: IncidentesPComponent,canActivate:[UserLoggedGuard]},
  { path: 'proyectos/:proyectoId/tareas',component: TareaspComponent,canActivate:[UserLoggedGuard]},
  { path: 'incidentes', component: IncidentesComponent,canActivate:[UserLoggedGuard]},
  { path: 'reportes/incidentes',component: IncidentesProyectosComponent,canActivate:[UserLoggedGuard]},
  { path: 'reportes/resueltos',component: IncidentesDesarrolladorComponent,canActivate:[UserLoggedGuard]},
  { path: 'reportes/:desarrolladorId/incidentes',component: DesarrolladorComponent,canActivate:[UserLoggedGuard]},
  { path: 'estados',component: EstadosComponent,canActivate:[UserLoggedGuard]},
  { path: 'asociaciones',component: AsociacionesComponent,canActivate:[UserLoggedGuard]},
  { path: 'importaciones',component: CargarIncidentesComponent,canActivate:[UserLoggedGuard]},
  { path: 'desarrollador',component: MisIncidentesComponent,canActivate:[UserLoggedGuard]},
  { path: 'tareas',component: VerTareasComponent,canActivate:[UserLoggedGuard]},
  { path: 'tareas/:tareaId/editar',component: ModificarTareaComponent,canActivate:[UserLoggedGuard]},
  { path: 'tareas/alta',component: AltaTareaComponent,canActivate:[UserLoggedGuard]},
  { path: '**', redirectTo: ''},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
