import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AltaUsuarioComponent } from './pages/alta-usuario/alta-usuario.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UsuariosComponent } from './pages/usuarios/usuarios.component';
import { RouterModule } from '@angular/router';
import { ListadoComponent } from './pages/listado/listado.component';





@NgModule({
  declarations: [
    AltaUsuarioComponent,
    UsuariosComponent,
    ListadoComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule
  ],
  exports:[
    AltaUsuarioComponent,
    UsuariosComponent
  ]
})
export class UsuariosModule { }
