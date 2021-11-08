import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CargarIncidentesComponent } from './pages/cargar-incidentes/cargar-incidentes.component';
import { PrimeNgModule } from '../prime-ng/prime-ng.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    CargarIncidentesComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    PrimeNgModule,
    SharedModule
  ],
  exports: [
    CargarIncidentesComponent
  ]
})
export class ImportacionesModule { }
