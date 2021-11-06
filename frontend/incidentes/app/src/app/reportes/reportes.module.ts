import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IncidentesProyectosComponent } from './pages/incidentes-proyectos/incidentes-proyectos.component';
import { IncidentesDesarrolladorComponent } from './pages/incidentes-desarrollador/incidentes-desarrollador.component';
import { ReportesComponent } from './pages/reportes/reportes.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PrimeNgModule } from '../prime-ng/prime-ng.module';
import { DesarrolladorComponent } from './pages/desarrollador/desarrollador.component';



@NgModule({
  declarations: [
    IncidentesProyectosComponent,
    IncidentesDesarrolladorComponent,
    ReportesComponent,
    DesarrolladorComponent
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
