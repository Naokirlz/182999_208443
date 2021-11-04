import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AltaUsuarioComponent } from './pages/alta-usuario/alta-usuario.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UsuariosComponent } from './pages/usuarios/usuarios.component';
import { RouterModule } from '@angular/router';
import { ListadoComponent } from './pages/listado/listado.component';
import { VerUsuariosComponent } from './pages/ver-usuarios/ver-usuarios.component';
import { PrimeNgModule } from '../prime-ng/prime-ng.module';
import { SharedModule } from '../shared/shared.module';





@NgModule({
  declarations: [
    AltaUsuarioComponent,
    UsuariosComponent,
    ListadoComponent,
    VerUsuariosComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    PrimeNgModule,
    SharedModule
  ],
  exports:[
    AltaUsuarioComponent,
    UsuariosComponent
  ]
})
export class UsuariosModule { }
