import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/pages/login/login.component';
import { LogoutComponent } from './login/pages/logout/logout.component';
import { AltaUsuarioComponent } from './usuarios/pages/alta-usuario/alta-usuario.component';
import { ReportesComponent } from './reportes/pages/reportes/reportes.component';
import { ProyectosComponent } from './proyectos/pages/proyectos/proyectos.component';
import { IncidentesComponent } from './incidentes/pages/incidentes/incidentes.component';
import { ImportacionesComponent } from './importaciones/pages/importaciones/importaciones.component';
import { EstadosComponent } from './estados/pages/estados/estados.component';
import { AsociacionesComponent } from './asociaciones/pages/asociaciones/asociaciones.component';
import { UsuariosComponent } from './usuarios/pages/usuarios/usuarios.component';
import { ListadoComponent } from './usuarios/pages/listado/listado.component';
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


const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
    pathMatch: 'full'
  },
  {
    path: 'salir',
    component: LogoutComponent,
  },
  {
    path: 'usuarios',
    component: VerUsuariosComponent,
  },
  {
    path: 'usuarios/alta',
    component: AltaUsuarioComponent,
  },
  {
    path: 'reportes',
    component: ReportesComponent,
  },
  {
    path: 'proyectos',
    component: VerProyectosComponent,
  },
  {
    path: 'proyectos/alta',
    component: AltaProyectoComponent,
  },
  {
    path: 'proyectos/:proyectoId/asignados',
    component: AsignadosComponent,
  },
  {
    path: 'proyectos/:proyectoId/editar',
    component: EditarProyectoComponent,
  },
  {
    path: 'proyectos/:proyectoId/incidentes',
    component: IncidentesPComponent,
  },
  {
    path: 'proyectos/:proyectoId/tareas',
    component: TareaspComponent,
  },
  {
    path: 'incidentes',
    component: IncidentesComponent,
  },
  {
    path: 'reportes/incidentes',
    component: IncidentesProyectosComponent,
  },
  {
    path: 'reportes/resueltos',
    component: IncidentesDesarrolladorComponent,
  },
  {
    path: 'reportes/:desarrolladorId/incidentes',
    component: DesarrolladorComponent,
  },
  {
    path: 'importaciones',
    component: ImportacionesComponent,
  },
  {
    path: 'estados',
    component: EstadosComponent,
  },
  {
    path: 'asociaciones',
    component: AsociacionesComponent,
  },
  {
    path: 'tareas',
    component: VerTareasComponent,
  },
  {
    path: 'tareas/alta',
    component: AltaTareaComponent,
  },
  {
    path: '**',
    redirectTo: ''
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
