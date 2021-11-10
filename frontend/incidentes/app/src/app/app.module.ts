import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';
import { LoginModule } from './login/login.module';
import { UsuariosModule } from './usuarios/usuarios.module';
import { PrimeNgModule } from './prime-ng/prime-ng.module';
import { ProyectosModule } from './proyectos/proyectos.module';
import { TareasModule } from './tareas/tareas.module';
import { ReportesModule } from './reportes/reportes.module';
import { DesarrolladorModule } from './desarrollador/desarrollador.module';
import { ImportacionesModule } from './importaciones/importaciones.module';
import { IncidentesModule } from './incidentes/incidentes.module';
import { TesterModule } from './tester/tester.module';



@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    SharedModule,
    LoginModule,
    UsuariosModule,
    TareasModule,
    PrimeNgModule,
    ProyectosModule,
    ReportesModule,
    DesarrolladorModule,
    ImportacionesModule,
    IncidentesModule,
    TesterModule   
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
