import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VerProyectosComponent } from './pages/ver-proyectos/ver-proyectos.component';
import { AltaProyectoComponent } from './pages/alta-proyecto/alta-proyecto.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PrimeNgModule } from '../prime-ng/prime-ng.module';
import { AsignadosComponent } from './pages/asignados/asignados.component';
import { EditarProyectoComponent } from './pages/editar-proyecto/editar-proyecto.component';
import { IncidentesPComponent } from './pages/incidentes/incidentes.component';
import { SharedModule } from '../shared/shared.module';
import { ProyectoInfoComponent } from './pages/proyecto-info/proyecto-info.component';



@NgModule({
  declarations: [
    AltaProyectoComponent,
    VerProyectosComponent,
    AsignadosComponent,
    EditarProyectoComponent,
    IncidentesPComponent,
    ProyectoInfoComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    PrimeNgModule
  ],
  exports:[
    AltaProyectoComponent,
    VerProyectosComponent
  ]
})
export class ProyectosModule { }
