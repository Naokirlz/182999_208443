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
import { VerUsuariosComponent } from './usuarios/pages/ver-usuarios/ver-usuarios.component';
import { AltaProyectoComponent } from './proyectos/pages/alta-proyecto/alta-proyecto.component';


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
  path: 'altaUsuario',
  component: AltaUsuarioComponent,
  
},
{
  path: 'verUsuarios',
  component: VerUsuariosComponent,
  
},


{
  path: 'reportes',
  component: ReportesComponent,
  
},
{
  path: 'proyectos',
  component: ProyectosComponent,
  
},
{
  path: 'altaProyecto',
  component: AltaProyectoComponent,
  
},
{
  path: 'incidentes',
  component: IncidentesComponent,
  
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
  path: '**',
  redirectTo:''
  
},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
