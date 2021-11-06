import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AltaUsuarioComponent } from './pages/alta-usuario/alta-usuario.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { VerUsuariosComponent } from './pages/ver-usuarios/ver-usuarios.component';
import { PrimeNgModule } from '../prime-ng/prime-ng.module';
import { SharedModule } from '../shared/shared.module';





@NgModule({
  declarations: [
    AltaUsuarioComponent,
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
    VerUsuariosComponent
  ]
})
export class UsuariosModule { }
