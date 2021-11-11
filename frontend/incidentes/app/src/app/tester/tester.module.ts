import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BugsTesterComponent } from './pages/bugs-tester/bugs-tester.component';
import { AltabugTesterComponent } from './pages/altabug-tester/altabug-tester.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PrimeNgModule } from '../prime-ng/prime-ng.module';
import { SharedModule } from 'primeng/api';
import { TareasComponent } from './pages/tareas/tareas.component';



@NgModule({
  declarations: [
    BugsTesterComponent,
    AltabugTesterComponent,
    TareasComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    PrimeNgModule,
    SharedModule
    
  ],
  exports: [
    BugsTesterComponent,
    AltabugTesterComponent
  ]
})
export class TesterModule { }
