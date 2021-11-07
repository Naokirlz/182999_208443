import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CargarIncidentesComponent } from './pages/cargar-incidentes/cargar-incidentes.component';
import { SharedModule } from 'primeng/api';
import { PrimeNgModule } from '../prime-ng/prime-ng.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



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
