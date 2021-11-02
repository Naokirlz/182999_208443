import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProyectosComponent } from './pages/proyectos/proyectos.component';
import { VerProyectosComponent } from './pages/ver-proyectos/ver-proyectos.component';
import { AltaProyectoComponent } from './pages/alta-proyecto/alta-proyecto.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PrimeNgModule } from '../prime-ng/prime-ng.module';



@NgModule({
  declarations: [
    ProyectosComponent,
    AltaProyectoComponent,
    VerProyectosComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    PrimeNgModule
  ],
  exports:[
    AltaProyectoComponent,
    VerProyectosComponent
  ]
})
export class ProyectosModule { }
