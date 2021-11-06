import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IncidentesProyectosComponent } from './pages/incidentes-proyectos/incidentes-proyectos.component';
import { IncidentesDesarrolladorComponent } from './pages/incidentes-desarrollador/incidentes-desarrollador.component';
import { ReportesComponent } from './pages/reportes/reportes.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PrimeNgModule } from '../prime-ng/prime-ng.module';



@NgModule({
  declarations: [
    IncidentesProyectosComponent,
    IncidentesDesarrolladorComponent,
    ReportesComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    PrimeNgModule
  ],
  exports:[
    IncidentesProyectosComponent,
    IncidentesDesarrolladorComponent
  ]
})
export class ReportesModule { }
