import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BugsTesterComponent } from './pages/bugs-tester/bugs-tester.component';
import { AltabugTesterComponent } from './pages/altabug-tester/altabug-tester.component';



@NgModule({
  declarations: [
    BugsTesterComponent,
    AltabugTesterComponent
  ],
  imports: [
    CommonModule
    
  ]
})
export class TesterModule { }
