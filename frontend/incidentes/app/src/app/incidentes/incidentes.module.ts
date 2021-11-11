import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IncidentesComponent } from './pages/incidentes/incidentes.component';
import { DetalleBugComponent } from './pages/detalle-bug/detalle-bug.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'primeng/api';
import { PrimeNgModule } from '../prime-ng/prime-ng.module';



@NgModule({
  declarations: [
    IncidentesComponent,
    DetalleBugComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    PrimeNgModule
  ]
})
export class IncidentesModule { }
