import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProyectosComponent } from './pages/proyectos/proyectos.component';
import { VerProyectosComponent } from './pages/ver-proyectos/ver-proyectos.component';
import { AltaProyectoComponent } from './pages/alta-proyecto/alta-proyecto.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PrimeNgModule } from '../prime-ng/prime-ng.module';
import { AsignadosComponent } from './pages/asignados/asignados.component';
import { EditarProyectoComponent } from './pages/editar-proyecto/editar-proyecto.component';



@NgModule({
  declarations: [
    ProyectosComponent,
    AltaProyectoComponent,
    VerProyectosComponent,
    AsignadosComponent,
    EditarProyectoComponent
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
