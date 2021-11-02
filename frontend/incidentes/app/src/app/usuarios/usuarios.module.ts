import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AltaUsuarioComponent } from './pages/alta-usuario/alta-usuario.component';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    AltaUsuarioComponent
  ],
  imports: [
    CommonModule,
    FormsModule
    
  ]
})
export class UsuariosModule { }
