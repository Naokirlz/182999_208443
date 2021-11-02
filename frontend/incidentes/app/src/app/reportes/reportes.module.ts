import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IncidentesProyectosComponent } from './pages/incidentes-proyectos/incidentes-proyectos.component';
import { IncidentesDesarrolladorComponent } from './pages/incidentes-desarrollador/incidentes-desarrollador.component';
import { ReportesComponent } from './pages/reportes/reportes.component';



@NgModule({
  declarations: [
    IncidentesProyectosComponent,
    IncidentesDesarrolladorComponent,
    ReportesComponent
  ],
  imports: [
    CommonModule
  ]
})
export class ReportesModule { }
