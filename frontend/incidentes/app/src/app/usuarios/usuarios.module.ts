import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AltaUsuarioComponent } from './pages/alta-usuario/alta-usuario.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UsuariosComponent } from './pages/usuarios/usuarios.component';
import { VerUsuariosComponent } from './pages/ver-usuarios/ver-usuarios.component';
import { PrimeNgModule } from '../prime-ng/prime-ng.module';





@NgModule({
  declarations: [
    AltaUsuarioComponent,
    UsuariosComponent,
    VerUsuariosComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    PrimeNgModule
  ],
  exports:[
    AltaUsuarioComponent,
    UsuariosComponent
  ]
})
export class UsuariosModule { }
