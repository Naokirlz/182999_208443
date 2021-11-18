import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AltabugTesterComponent } from './pages/altabug-tester/altabug-tester.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PrimeNgModule } from '../prime-ng/prime-ng.module';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    AltabugTesterComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    PrimeNgModule,
    SharedModule
    
  ],
  exports: [
    AltabugTesterComponent
  ]
})
export class TesterModule { }
