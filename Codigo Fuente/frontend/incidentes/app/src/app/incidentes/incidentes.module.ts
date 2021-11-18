import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DetalleBugComponent } from './pages/detalle-bug/detalle-bug.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PrimeNgModule } from '../prime-ng/prime-ng.module';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
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
