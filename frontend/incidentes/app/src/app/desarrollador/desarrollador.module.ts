import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MisIncidentesComponent } from './pages/mis-incidentes/mis-incidentes.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'primeng/api';
import { PrimeNgModule } from '../prime-ng/prime-ng.module';



@NgModule({
  declarations: [
    MisIncidentesComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    PrimeNgModule
  ],
  exports:[
    MisIncidentesComponent,
  ]
})
export class DesarrolladorModule { }
