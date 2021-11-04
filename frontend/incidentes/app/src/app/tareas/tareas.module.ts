import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PrimeNgModule } from '../prime-ng/prime-ng.module'
import { SharedModule } from '../shared/shared.module';
import { VerTareasComponent } from './pages/ver-tareas/ver-tareas.component';



@NgModule({
  declarations: [
    VerTareasComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    PrimeNgModule,
    SharedModule
  ],
  exports: [
    VerTareasComponent
  ]
})
export class TareasModule { }