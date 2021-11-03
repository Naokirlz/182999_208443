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
import { VerTareasComponent } from './tareas/pages/ver-tareas/ver-tareas.component';
import { AltaTareaComponent } from './tareas/pages/alta-tarea/alta-tarea.component';



@NgModule({
  declarations: [
    AppComponent,
    VerTareasComponent,
    AltaTareaComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    SharedModule,
    LoginModule,
    UsuariosModule,
    PrimeNgModule,
    ProyectosModule    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
