import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SidebarComponent } from './sidebar/sidebar.component';
import { RouterModule } from '@angular/router';
import { MenuComponent } from './menu/menu.component';
import { PrimeNgModule } from '../prime-ng/prime-ng.module';


@NgModule({
  declarations: [
    SidebarComponent,
    MenuComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    PrimeNgModule
    
  ],
  exports: [
    SidebarComponent,
    MenuComponent
  ]
})
export class SharedModule { }
